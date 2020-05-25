using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitTime : BaseCompornent
{

    GameObject sun;
    GameObject moon;

    SunController sunCtrl;
    MoonController moonCtrl;

    private Timer time = new Timer(3, 0);

    // Start is called before the first frame update
    void Start()
    {
        time.Start();

        sun = GameObject.Find("Sun");
        sunCtrl = sun.GetComponent<SunController>();

        moon = GameObject.Find("Moon");
        moonCtrl = moon.GetComponent<MoonController>();
    }

    // Update is called once per frame
    void Update()
    {
        time.Update();
        if (time.IsPause()) return;

        //残り時間（半分）による１フレームあたりの移動量
        var add = 1.0f / time.GetToInitSeconds() / 60.0f * 2.0f;

        //太陽の処理
        sunCtrl.Move(add);

        //残り時間が半分なら
        if (time.GetToSeconds() > time.GetToInitSeconds() / 2.0f) return;

        //月の処理
        moonCtrl.Move(add);
    }

    private void OnGUI()
    {
        if (!GameDirector.DEBUG) return;
        GUI.TextArea(new Rect(0, 50, 100, 50), "time : " + time.GetMinute() + " : " + time.GetSeconds());

        if (GUI.Button(new Rect(200, 0, 50, 50), "Stop"))
        {
            time.Stop();
        }

        if (GUI.Button(new Rect(150, 0, 50, 50), "Start"))
        {
            time.ReStart();
        }

        if (time.IsFinish())
        {
            GUI.TextArea(new Rect(100, 50, 100, 50), "-- Finish --");
        }
    }

    public bool IsFinish() { return time.IsFinish(); }
}
