using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFlyRingController : BaseCompornent
{
    [SerializeField, Range(0.0f, 1.0f)] float testFlame = 1.0f;

    [SerializeField] Color startColor = new Color(1, 1, 1, 1);
    [SerializeField] Color centerColor = new Color(1, 1, 1, 1);
    [SerializeField] Color finishColor = new Color(1, 1, 1, 1);

    [SerializeField] float ScaleSpeed = 0.001f;

    [SerializeField] bool centerColor_Enable = true;


    private float MinScale = 1.0f;
    private float MaxScale = 1.5f;
   
    bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        isFinish = false;
        ScaleX = ScaleY = MaxScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (centerColor_Enable)
        {
            MaterialColor = Interpolation.BezierCurve(startColor, centerColor, finishColor, testFlame);
        }
        else
        {
            MaterialColor = Color.Lerp(startColor, finishColor, testFlame);
        }

        var nowScale = ScaleX;
        nowScale = Mathf.Max(nowScale - ScaleSpeed, MinScale);
        ScaleX = ScaleY = nowScale;
    }
}
