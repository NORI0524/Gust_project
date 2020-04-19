﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class GameDirector : BaseCompornent
{
    public const bool DEBUG = false;

    //TODO:リザルトにでランクを付ける際に指標になる変数

    //満足度の合計
    public static int totalSatisfyValue;
    //お客さんの合計
    public static int totalCustomerNum;

    //入浴中のお客さんの数
    public static int bathingCustomerNum;

    //制限時間
    GameObject limitTime;
    LimitTime limitTimeCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        totalSatisfyValue = 0;
        totalCustomerNum = 0;
        bathingCustomerNum = 0;


        limitTime = GameObject.Find("LimitTimer");
        limitTimeCtrl = limitTime.GetComponent<LimitTime>();

        SoundMan.Instance.PlayBGM("bgm", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        SoundMan.Instance.Update();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    public void OnGUI()
    {
        if (!DEBUG) return;
        GUI.TextArea(new Rect(0, 100, 100, 50), "BathingNum : " + bathingCustomerNum);
        GUI.TextArea(new Rect(0, 200, 100, 50), "totalSatisfy : " + totalSatisfyValue);
    }
}
