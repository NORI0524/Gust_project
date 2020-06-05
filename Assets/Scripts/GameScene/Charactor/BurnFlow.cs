using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnFlow : BaseCompornent
{

    [SerializeField, Range(0.0f, 1.0f)] float testFlame = 1.0f;

    [SerializeField] Color startColor = new Color(1, 1, 1, 1);
    [SerializeField] Color centerColor = new Color(1, 1, 1, 1);
    [SerializeField] Color finishColor = new Color(1, 1, 1, 1);

    [SerializeField] bool centerColor_Enable = true;

    private int burnedTime = 0;

    private float flame;

    // Start is called before the first frame update
    void Start()
    {
        var baseChara = GetComponent<BaseCharactorController>();
        burnedTime = baseChara.BestFriedTime;
        flame = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var add = 1.0f / 60.0f / burnedTime;
        flame += add;

        if (centerColor_Enable)
        {
            MaterialColor = Interpolation.BezierCurve(startColor, centerColor, finishColor, testFlame);
        }
        else
        {
            MaterialColor = Color.Lerp(startColor, finishColor, testFlame);
        }
    }
}
