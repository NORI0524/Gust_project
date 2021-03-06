﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : BaseCompornent
{

    private Vector3 start = new Vector3(-10.0f, 1.0f, 3.75f);
    private Vector3 middle = new Vector3(0.0f, 7.0f, 3.75f);
    private Vector3 end = new Vector3(10.0f, 1.0f, 3.75f);

    private float frame;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float add)
    {
        frame += add;
        Position = Interpolation.BezierCurve(start, middle, end, frame);
    }

    public float GetFrame()
    {
        return frame;
    }
}
