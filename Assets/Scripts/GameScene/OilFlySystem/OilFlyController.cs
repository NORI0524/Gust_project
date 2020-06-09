using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFlyController : BaseCompornent
{
    //鍋蓋
    SaucePanManager saucePanManager = null;

    SpriteRenderer spriteRenderer;

    private Transparent destroyTrans = null;

    [SerializeField] Sprite NormalSprite = null;
    [SerializeField] Sprite DamageSprite = null;

    [SerializeField] int OilFlyHitTime = 4;
    [SerializeField] int DestroyOilFlyTime = 3;
    [SerializeField] int OilFlyRotateSpeed = 5;

    bool isHit, isProtect;

    FadeValue scaleFade;

    SpawnFactory oilFac;

    // Start is called before the first frame update
    void Start()
    {
        saucePanManager = GetComponent<SaucePanManager>("Saucepan_Lid_Icon");


        var obj = GameObject.Find("OilFlyManager");
        oilFac = obj.GetComponent<SpawnFactory>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = NormalSprite;

        scaleFade = new FadeValue(0.01f, OilFlyHitTime, 0.01f, 1.0f);
        scaleFade.isPlus = true;

        isHit = false;
        isProtect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //posFade.Update();
        scaleFade.Update();

        if (!isProtect)
        {
            DegAngle += OilFlyRotateSpeed;
            ScaleX = ScaleY = scaleFade.GetCurrentValue();
        }

        if (ScaleX >= 1.0f)
        {
            isHit = true;
            GameDirector.oilFlyHitNum++;
            CreateDestroyTrans();
            
        }

        //防いだ時の処理
        if(saucePanManager.IsAlive)
        {
            if (!isProtect)
            {
                isProtect = true;
                CreateDestroyTrans();
            }
        }

        //消失処理
        if (destroyTrans == null) return;
        if (destroyTrans.IsFinish())
        {
            oilFac.Decrease();
            Destroy(gameObject);
        }

        //食らった時の処理
        if (!isHit) return;
        
        DegAngle = 0;
        ScaleX = ScaleY = scaleFade.maxValue;
        spriteRenderer.sprite = DamageSprite;
    }

    private void CreateDestroyTrans()
    {
        if (destroyTrans != null) return;
        gameObject.AddComponent<Transparent>();
        destroyTrans = GetComponent<Transparent>();
        destroyTrans.TransparentSeconds = DestroyOilFlyTime;
    }

    public int GetOilFlyhitTime()
    {
        return OilFlyHitTime;
    }
}
