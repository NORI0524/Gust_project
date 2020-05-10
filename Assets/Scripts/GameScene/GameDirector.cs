using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class GameDirector : BaseCompornent
{
    public static bool DEBUG = false;

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

    //開始、終了
    GameObject startObj;
    GameObject finishObj;

    bool isFinish;
    
    // Start is called before the first frame update
    void Start()
    {
        totalSatisfyValue = 0;
        totalCustomerNum = 0;
        bathingCustomerNum = 0;

        startObj = GameObject.Find("Start");
        finishObj = GameObject.Find("Finish");

        //アクティブ無効
        finishObj.SetActive(false);


        limitTime = GameObject.Find("LimitTimer");
        limitTimeCtrl = limitTime.GetComponent<LimitTime>();

        SoundMan.Instance.PlayBGM("bgm", 3.0f);
        SoundMan.Instance.PlaySE("start");

        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { DEBUG = !DEBUG; }

        //開始が終わってるならアクティブ無効
        if (startObj.GetComponent<Transparent>().IsFinish()) startObj.SetActive(false);

        

        //ゲーム終了
        if (limitTimeCtrl.IsFinish() && !isFinish)
        {
            finishObj.SetActive(true);
            SoundMan.Instance.PlaySE("finish");
            isFinish = true;
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
