﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class GameDirector : BaseCompornent
{
    public static bool DEBUG = false;

    SceneFadeInSteam sceneFadeIn = null;
    SceneFadeOutSteam sceneFadeOut = null;

    //TODO:リザルトにでランクを付ける際に指標になる変数

    //満足度の合計
    public static int totalSatisfyValue;
    //お客さんの合計
    public static int totalCustomerNum;

    //入浴中のお客さんの数
    public static int bathingCustomerNum;

    //油跳ねを食らった回数
    public static int oilFlyHitNum;

    //制限時間
    GameObject limitTime;
    LimitTime limitTimeCtrl;

    //開始、終了
    GameObject startObj;
    GameObject finishObj;

    public static bool isStart;
    public static bool isFinish;

    Timer finishBreakTimer = new Timer(3);


    // Start is called before the first frame update
    void Start()
    {
        totalSatisfyValue = 0;
        totalCustomerNum = 0;
        bathingCustomerNum = 0;
        oilFlyHitNum = 0;

        startObj = GameObject.Find("Start");
        finishObj = GameObject.Find("Finish");

        //アクティブ無効
        startObj.SetActive(false);
        finishObj.SetActive(false);


        limitTime = GameObject.Find("LimitTimer");
        limitTimeCtrl = limitTime.GetComponent<LimitTime>();
        sceneFadeIn = GetComponent<SceneFadeInSteam>("SceneFadeInSteamManager");
        sceneFadeOut = GetComponent<SceneFadeOutSteam>("SceneFadeOutSteamManager");

        if (sceneFadeOut == null)
        {
            Debug.Log("nullのまま");
        }

        sceneFadeOut.IsStart = true;

        SoundMan.Instance.PlayBGM("bgm", 3.0f);

        isStart = false;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { DEBUG = !DEBUG; }

        //フェードアウトが終わったらゲーム開始
        if (sceneFadeOut.IsFinish && !isStart)
        {
            isStart = true;
            SoundMan.Instance.PlaySE("start");
            startObj.SetActive(true);
        }

        //開始が終わってるならアクティブ無効
        if (startObj.GetComponent<Transparent>().IsFinish()) startObj.SetActive(false);


        finishBreakTimer.Update();

        //ゲーム終了
        if (limitTimeCtrl.IsFinish() && !isFinish)
        {
            finishObj.SetActive(true);
            SoundMan.Instance.PlaySE("finish");
            isFinish = true;

            //Factoryクラスを停止
            var customerFac = GetComponent<SpawnFactory>("CustomerManager");
            customerFac.OffActive();
            var oilFac = GetComponent<SpawnFactory>("OilFlyManager");
            oilFac.OffActive();


            finishBreakTimer.Start();
        }

        if (finishBreakTimer.IsFinish())
        {
            var sceneFadeIn = GetComponent<SceneFadeInSteam>("SceneFadeInSteamManager");
            sceneFadeIn.IsStart = true;
        }

        if (sceneFadeIn.IsFinish)
        {
            SceneManager.LoadScene("ResultScene");
        }

        //音響
        SoundMan.Instance.PlaySE("fire");
        if (bathingCustomerNum > 0) SoundMan.Instance.PlaySE("fried");

        SoundMan.Instance.Update();
    }

    public void OnGUI()
    {
        if (!DEBUG) return;
        GUI.TextArea(new Rect(0, 100, 100, 50), "BathingNum : " + bathingCustomerNum);
        GUI.TextArea(new Rect(0, 200, 100, 50), "totalSatisfy : " + totalSatisfyValue);


        if (GUI.Button(new Rect(0, 300, 75, 50), "Reset"))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (GUI.Button(new Rect(100, 300, 75, 50), "Title"))
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (GUI.Button(new Rect(200, 300, 75, 50), "Result"))
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
