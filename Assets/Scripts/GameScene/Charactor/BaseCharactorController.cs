using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharaFaceState = CharactorFaceController.FaceState;

using SoundMan = Singleton<SoundManager>;

public class BaseCharactorController : BaseCompornent
{
    //Sprite管理用
    SpriteRenderer spriteRenderer;

    //public宣言しinspectorで設定可能にする
    public Sprite NormalSprite;
    public Sprite BathingSprite;
    public Sprite FriedSprite;
    public Sprite FriedBathingSprite;

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

        public const uint Intrusion = 1 << 4;

        public const uint Death = 1 << 5;

        public const uint Drag = 1 << 5;
    }

    //表情State
    CharaFaceState face;

    //温泉の温度
    ThemometerController themoCtrl;

    //温度管理
    ThemoManager themoMan;


    //満足度
    SatisfactionLevel satisfy = new SatisfactionLevel();

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
    }

    // Update is called once per frame
    void Update()
    {
        //表情
        if (satisfy.GetSatisfyValue() >= 50) face = CharaFaceState.Normal;
        if (satisfy.GetSatisfyValue() >= 75) face = CharaFaceState.Smile;
        if (satisfy.GetSatisfyValue() >= 100) face = CharaFaceState.Cat;

       

        if (state.CheckBitOR(State.Bathing | State.FriedBathing))
        {
            var nowFirePower = themoCtrl.GetFirePower();
            if (nowFirePower >= themoMan.GetBestFirePowerMin() && nowFirePower <= themoMan.GetBestFirePowerMax())
            {
                satisfy.AddSatisfy();
            }
            else
            {
                satisfy.SubSatisfy();
            }
        }
        if (state.CheckBit(State.Drag)) return;
        if (state.CheckBitOR(State.Normal))
        {
            PosX = Mathf.Clamp(PosX + 0.05f, -8.0f, -5.0f);
        }
        if (state.CheckBit(State.Fried))
        {
            PosX += 0.05f;
        }
        if(PosX > 10.0f)
        {
            state.AddBit(State.Death);
        }
        if (state.CheckBit(State.Death))
        {
            customerFac.Decrease();
            Destroy(gameObject);
        }
    }

    public void OnDown()
    {
        Vector3 screenPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        oldWorldPos = worldPos;

        SoundMan.Instance.PlaySE("tap");
    }

    public void OnDrag()
    {
        Vector3 screenPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        PosX += worldPos.x - oldWorldPos.x;
        PosY += worldPos.y - oldWorldPos.y;

        oldWorldPos = worldPos;

        if (state.CheckBitOR(State.Bathing | State.FriedBathing))
        {
            state.FoldBit(State.Bathing | State.FriedBathing);
            state.AddBit(State.Fried);
            spriteRenderer.sprite = FriedSprite;

            GameDirector.bathingCustomerNum--;
        }

        state.AddBit(State.Drag);
    }

    public void OnDrop()
    {
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
        state.FoldBit(State.Intrusion);
        if (collision.CompareTag("Bathtub"))
            state.AddBit(State.Intrusion);
    }

    public int GetSatisfy()
    {
        return satisfy.GetSatisfyValue();
    }
    public CharaFaceState GetFaceState()
    {
        return face;
    }
}
