using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OilFlyController : BaseCompornent
{
    SpriteRenderer spriteRenderer;


    //鍋蓋
    SaucePanManager saucePanManager = null;

    private Transparent destroyTrans = null;

    [SerializeField] Sprite NormalSprite = null;
    [SerializeField] Sprite DamageSprite = null;

    [SerializeField] int OilFlyHitTime = 4;
    [SerializeField] int OilFlyRotateSpeed = 5;
    [SerializeField] int OilFlyBowHeight = 3;

    bool isHit;

    FadeValue scaleFade, posFade;

    SpawnFactory oilFac;

    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.Find("OilFlyManager");
        oilFac = obj.GetComponent<SpawnFactory>();

        saucePanManager = GetComponent<SaucePanManager>("Saucepan_Lid_icon");

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = NormalSprite;

        scaleFade = new FadeValue(0.01f, OilFlyHitTime, 0.01f, 1.0f);
        posFade = new FadeValue(PosY, OilFlyHitTime / 2, PosY, PosY + OilFlyBowHeight);


        posFade.isPlus = true;
        scaleFade.isPlus = true;
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        DegAngle += OilFlyRotateSpeed;

        posFade.Update();
        scaleFade.Update();

        PosY = posFade.GetCurrentValue();
        ScaleX = ScaleY = scaleFade.GetCurrentValue();

        if(saucePanManager)

        if(ScaleX >= 1.0f)
        {
            isHit = true;
            gameObject.AddComponent<Transparent>();

            destroyTrans = GetComponent<Transparent>();
        }

        if (isHit)
        {
            DegAngle = 0;
            ScaleX = ScaleY = scaleFade.maxValue;
            PosY = posFade.minValue;
            spriteRenderer.sprite = DamageSprite;

            if (!destroyTrans.IsFinish()) return;

            oilFac.Decrease();
            Destroy(gameObject);
        }
    }
}
