using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : BaseCompornent
{

    SunController sunCtrl;

    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.Find("Sun");
        sunCtrl = obj.GetComponent<SunController>();
    }

    // Update is called once per frame
    void Update()
    {
        MaterialColor = Interpolation.BezierCurve(Color.cyan, Color.blue, Colors.Navy, sunCtrl.GetFrame());
    }
}
