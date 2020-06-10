using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFlyRingController : BaseCompornent
{
    [SerializeField] Color startColor = new Color(1, 1, 1, 1);
    [SerializeField] Color centerColor = new Color(1, 1, 1, 1);
    [SerializeField] Color finishColor = new Color(1, 1, 1, 1);

    [SerializeField] float ScaleSpeed = 0.001f;

    OilFlyController parentCtrl = null;

    [SerializeField] float MinScale = 1.0f;
    [SerializeField] float MaxScale = 1.5f;

    private float frame;
    private Transparent destroyTrans = null;

    bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        isFinish = false;
        ScaleX = ScaleY = MaxScale;
        frame = 0.0f;

        var obj = gameObject.transform.parent.gameObject;
        parentCtrl = obj.GetComponent<OilFlyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentCtrl.IsProtect() || parentCtrl.IsHit())
        {
            CreateDestroyTrans();
            return;
        }

        var add = 1.0f / 60.0f / (float)parentCtrl.GetOilFlyhitTime();
        frame += add;
        MaterialColor = Interpolation.BezierCurve(startColor, centerColor, finishColor, frame);

        var nowScale = ScaleX;
        nowScale = Mathf.Max(nowScale - ScaleSpeed, MinScale);
        ScaleX = ScaleY = nowScale;
    }

    private void CreateDestroyTrans()
    {
        if (destroyTrans != null) return;
        gameObject.AddComponent<Transparent>();
        destroyTrans = GetComponent<Transparent>();
        destroyTrans.TransparentSeconds = parentCtrl.GetDestroyOilFlyTime();
    }
}
