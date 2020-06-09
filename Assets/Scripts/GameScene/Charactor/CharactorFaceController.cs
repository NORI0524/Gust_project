
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorFaceController : BaseCompornent
{
    //Sprite管理用
    SpriteRenderer spriteRenderer;

    //public宣言しinspectorで設定可能にする
    [SerializeField] Sprite NormalSprite = null;
    [SerializeField] Sprite SmileSprite = null;
    [SerializeField] Sprite AngerSprite = null;
    [SerializeField] Sprite SleepSprite = null;
    [SerializeField] Sprite SurpriseSprite = null;
    [SerializeField] Sprite NothingSprite = null;
    [SerializeField] Sprite NotGood_FirstSprite = null;
    [SerializeField] Sprite NotGood_SecondSprite = null;
    [SerializeField] Sprite NotGood_ThirdSprite = null;
    [SerializeField] Sprite CatSprite = null;
    [SerializeField] Sprite PunsukaSprite = null;

    //表情
    public enum FaceState
    {
        Normal,
        Smile,
        Anger,
        Sleep,
        Surprise,
        Nothing,
        NotGood_First,
        NotGood_Second,
        NotGood_Third,
        Cat,
        Punsuka,
        Num
    }

    //親である本体
    BaseCharactorController parentCtrl;

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのSprteRender取得
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        var objParent = gameObject.transform.parent.gameObject;
        parentCtrl = objParent.GetComponent<BaseCharactorController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (parentCtrl.GetFaceState())
        {
            case FaceState.Normal:
                spriteRenderer.sprite = NormalSprite;
                break;

            case FaceState.Smile:
                spriteRenderer.sprite = SmileSprite;
                break;

            case FaceState.Anger:
                spriteRenderer.sprite = AngerSprite;
                break;

            case FaceState.Sleep:
                spriteRenderer.sprite = SleepSprite;
                break;

            case FaceState.Surprise:
                spriteRenderer.sprite = SurpriseSprite;
                break;

            case FaceState.Nothing:
                spriteRenderer.sprite = NothingSprite;
                break;

            case FaceState.NotGood_First:
                spriteRenderer.sprite = NotGood_FirstSprite;
                break;

            case FaceState.NotGood_Second:
                spriteRenderer.sprite = NotGood_SecondSprite;
                break;

            case FaceState.NotGood_Third:
                spriteRenderer.sprite = NotGood_ThirdSprite;
                break;

            case FaceState.Cat:
                spriteRenderer.sprite = CatSprite;
                break;

            case FaceState.Punsuka:
                spriteRenderer.sprite = PunsukaSprite;
                break;

            default:
                spriteRenderer.sprite = NormalSprite;
                break;
        }
    }
}
