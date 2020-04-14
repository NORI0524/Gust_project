using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : BaseCompornent
{
    private int frameCnt;
    private float prevTime;
    private float fps;

    // Start is called before the first frame update
    void Start()
    {
        frameCnt = 0;
        prevTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        frameCnt++;
        float time = Time.realtimeSinceStartup - prevTime;

        if(time >= 0.5f)
        {
            fps = frameCnt / time;
            frameCnt = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }

    //public void OnGUI()
    //{
    //    GUI.TextArea(new Rect(1100, 0, 100, 50), "FPS : " + fps);
    //}
}
