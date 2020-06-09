using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEffectController : BaseCompornent
{
    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite NormalEffectSprite = null;
    [SerializeField] Sprite BathingEffectSprite = null;

    [SerializeField] Color startColor = new Color(1, 1, 1, 1);
    [SerializeField] Color centerColor = new Color(1, 1, 1, 1);
    [SerializeField] Color finishColor = new Color(1, 1, 1, 1);

    BaseCharactorController parentCtrl = null;

    bool isBathing;

    float effectFrame;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        var objParent = gameObject.transform.parent.gameObject;
        parentCtrl = objParent.GetComponent<BaseCharactorController>();

        isBathing = false;
        effectFrame = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentCtrl.GetBathing())
        {
            spriteRenderer.sprite = BathingEffectSprite;
            isBathing = true;
            effectFrame += parentCtrl.AddFrameToFriedTime();
            MaterialColor = Interpolation.BezierCurve(startColor, centerColor, finishColor, effectFrame);
        }

        if(isBathing && parentCtrl.GetGrab())
        {
            spriteRenderer.sprite = NormalEffectSprite;
        }
    }
}
