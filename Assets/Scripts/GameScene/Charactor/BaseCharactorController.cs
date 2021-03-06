﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharaFaceState = CharactorFaceController.FaceState;

using SoundMan = Singleton<SoundManager>;

public class BaseCharactorController : BaseCompornent
{
    //Sprite管理用
    SpriteRenderer spriteRenderer;

    //public宣言しinspectorで設定可能にする
    [SerializeField] Sprite NormalSprite = null;
    [SerializeField] Sprite BathingSprite = null;
    [SerializeField] Sprite FriedSprite = null;
    [SerializeField] Sprite FriedBathingSprite = null;

    //リアクションPrefabs
    [SerializeField] GameObject[] ReactionPrefabs = new GameObject[3];

    //アニメーター
    Animator animator = null;

    //Factory
    SpawnFactory customerFac;

    //State
    BitFlag state = new BitFlag();
    class State
    {
        public const uint Normal = 1 << 0;
        public const uint Bathing = 1 << 1;
        public const uint Fried = 1 << 2;
        public const uint FriedBathing = 1 << 3;
        public const uint Burn = 1 << 4;

        public const uint Intrusion = 1 << 5;

        public const uint Death = 1 << 6;

        public const uint Drag = 1 << 7;
    }

    enum ReactionState
    {
        Good,
        Bad,
        Shock,
        None,
        Num
    }

    //リアクション
    ReactionState reactionState;
    Timer reactionTimer;

    Vector3 reactionDisp;

    //表情State
    CharaFaceState face;

    //温泉の温度
    ThemometerController themoCtrl;

    //温度管理
    ThemoManager themoMan;

    //揚げ時間（焦げるまで）
    [SerializeField, Range(0, 60)] public int BurnFriedTime = 10;

    //揚げ時間タイマー
    Timer friedTimer = null;

    //満足度
    SatisfactionLevel satisfy = new SatisfactionLevel();

    //フライド化の目安満足度
    [SerializeField] int FriedSatisfy = 70;

    Vector3 oldWorldPos;

    // Start is called before the first frame update
    void Start()
    {
        state.FoldALLBit();
        state.AddBit(State.Normal);

        face = CharaFaceState.Normal;

        //このオブジェクトのSprteRender取得
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        var obj = GameObject.Find("Themometer_main");
        themoCtrl = obj.GetComponent<ThemometerController>();

        obj = GameObject.Find("ThemoRangeGauge");
        themoMan = obj.GetComponent<ThemoManager>();

        obj = GameObject.Find("CustomerManager");
        customerFac = obj.GetComponent<SpawnFactory>();

        animator = GetComponent<Animator>();
        animator.SetBool("Grab", false);
        animator.SetBool("Walk", false);


        //揚げ時間
        friedTimer = new Timer(BurnFriedTime);
        friedTimer.Start();


        //リアクション
        reactionState = ReactionState.None;
        reactionTimer = new Timer(6);
        reactionDisp = new Vector3(0.5f, 0.5f, PosZ - 0.01f); ;
        reactionTimer.EnabledLoop();
        reactionTimer.Start();

        GameDirector.totalCustomerNum++;
    }

    // Update is called once per frame
    void Update()
    {
        //入浴フラグ折る
        state.FoldBit(State.Intrusion);

        //TODO:客ごとに共通の適正温度にするなら
        var BestFirePowerMin = themoMan.GetBestFirePowerMin();
        var BestFirePowerMax = themoMan.GetBestFirePowerMax();

        //入浴中なら
        if (state.CheckBitOR(State.Bathing | State.FriedBathing))
        {
            animator.SetBool("Grab", false);
            animator.SetBool("Walk", false);
            animator.enabled = false;

            friedTimer.Update();
            reactionTimer.Update();

            //満足度の増減
            var nowFirePower = themoCtrl.GetFirePower();
            if (nowFirePower >= BestFirePowerMin && nowFirePower <= BestFirePowerMax)
            {
                if (!state.CheckBit(State.Burn))
                {
                    satisfy.AddSatisfy();
                    reactionState = ReactionState.Good;
                }
            }
            else
            {
                satisfy.SubSatisfy();
                reactionState = ReactionState.Shock;
            }

            //焦げたら満足度減少
            if (state.CheckBit(State.Burn))
            {
                satisfy.SubSatisfy();
            }

            //揚げ時間を越したら焦げ状態
            if (friedTimer.IsFinish())
            {
                state.AddBit(State.Burn);
                reactionState = ReactionState.Bad;
            }

            if (reactionTimer.IsFinish())
            {
                Reaction();
            }

            //入浴中のお客さんが揚がるかどうか
            if (state.CheckBitOR(State.Bathing) && satisfy.SatisfyValue > FriedSatisfy)
            {
                state.FoldBit(State.Bathing);
                state.AddBit(State.FriedBathing);
                spriteRenderer.sprite = FriedBathingSprite;
            }
        }

        //表情
        FacialExpression();

        if (state.CheckBit(State.Drag)) return;

        //移動
        Move();

        //画面外で破棄
        ScreenOut();
    }

    public void OnDown()
    {
        if (!GameDirector.isStart || GameDirector.isFinish) return;

        Vector3 screenPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        oldWorldPos = worldPos;

        SoundMan.Instance.PlaySE("tap");
    }

    public void OnDrag()
    {
        if (!GameDirector.isStart || GameDirector.isFinish) return;

        Vector3 screenPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        PosX += worldPos.x - oldWorldPos.x;
        PosY += worldPos.y - oldWorldPos.y;

        oldWorldPos = worldPos;

        animator.enabled = true;
        animator.SetBool("Grab", true);
        animator.SetBool("Walk", false);

        if (state.CheckBit(State.Bathing))
        {
            state.FoldBit(State.Bathing);
            state.AddBit(State.Normal);
            spriteRenderer.sprite = NormalSprite;

            GameDirector.bathingCustomerNum--;
        }
        else if(state.CheckBit(State.FriedBathing))
        {
            state.FoldBit(State.FriedBathing);
            state.AddBit(State.Fried);
            spriteRenderer.sprite = FriedSprite;

            GameDirector.bathingCustomerNum--;
        }

        state.AddBit(State.Drag);
    }

    public void OnDrop()
    {
        if (!GameDirector.isStart || GameDirector.isFinish) return;

        if (state.CheckBit(State.Normal | State.Intrusion))
        {
            state.FoldBit(State.Normal | State.Intrusion);
            state.AddBit(State.Bathing);
            spriteRenderer.sprite = BathingSprite;

            GameDirector.bathingCustomerNum++;
        }

        if (state.CheckBit(State.Fried | State.Intrusion))
        {
            state.FoldBit(State.Fried | State.Intrusion);
            state.AddBit(State.FriedBathing);
            spriteRenderer.sprite = FriedBathingSprite;

            GameDirector.bathingCustomerNum++;
        }

        state.FoldBit(State.Drag);
    }

    public void RelayOnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bathtub"))
            state.AddBit(State.Intrusion);
    }

    public int GetSatisfy()
    {
        return satisfy.SatisfyValue;
    }
    public CharaFaceState GetFaceState()
    {
        return face;
    }

    private void Move()
    {
        if (state.CheckBit(State.Normal) && !state.CheckBit(State.Burn))
        {
            PosX = Mathf.Clamp(PosX + 0.02f, -8.0f, -5.5f);
        }
        if (state.CheckBit(State.Fried) || state.CheckBit(State.Burn | State.Normal))
        {
            PosX += 0.02f;
        }

        if (state.CheckBitOR(State.Bathing | State.FriedBathing)) return;

        animator.enabled = true;
        animator.SetBool("Grab", false);
        animator.SetBool("Walk", true);
    }

    //表情
    private void FacialExpression()
    {
        var satisfyVal = satisfy.SatisfyValue;
        if (satisfyVal >= 50) face = CharaFaceState.Normal;
        if (satisfyVal >= 75) face = CharaFaceState.Smile;
        if (satisfyVal >= 100) face = CharaFaceState.Cat;

        if (state.CheckBit(State.Burn)) face = CharaFaceState.Anger;
    }

    private void Reaction()
    {
        if (reactionState == ReactionState.Good)
        {
            GameObject obj = Instantiate(ReactionPrefabs[(int)ReactionState.Good]) as GameObject;
            obj.transform.position = new Vector3(PosX + reactionDisp.x, PosY + reactionDisp.y, PosZ + reactionDisp.z);
        }
        if (reactionState == ReactionState.Bad)
        {
            GameObject obj = Instantiate(ReactionPrefabs[(int)ReactionState.Bad]) as GameObject;
            obj.transform.position = new Vector3(PosX + reactionDisp.x, PosY + reactionDisp.y, PosZ + reactionDisp.z);
        }
        if (reactionState == ReactionState.Shock)
        {
            GameObject obj = Instantiate(ReactionPrefabs[(int)ReactionState.Shock]) as GameObject;
            obj.transform.position = new Vector3(PosX + reactionDisp.x, PosY + reactionDisp.y, PosZ + reactionDisp.z);
        }
    }

    private void ScreenOut()
    {
        if (PosX > 10.0f)
        {
            state.AddBit(State.Death);
        }
        if (state.CheckBit(State.Death))
        {
            customerFac.Decrease();
            Destroy();
        }
    }

    public bool GetBathing()
    {
        return state.CheckBitOR(State.Bathing | State.FriedBathing);
    }

    public bool GetGrab()
    {
        return state.CheckBit(State.Drag);
    }

    public float AddFrameToFriedTime()
    {
        float add = 1.0f / friedTimer.GetInitSeconds() / 60.0f;
        return add;
    }
}
