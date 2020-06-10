using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rank = RankManager.Rank;
using SoundMan = Singleton<SoundManager>;

public class ResultDirector : BaseCompornent
{
    SceneFadeInSteam sceneFadeIn;
    SceneFadeOutSteam sceneFadeOut;

    private int money;
    private Rank rank;
    public Rank RankResult { get { return rank; } }
    private float CharaSpace = 2.0f;


    //客の演出
    private bool CharacterMoveFlg = false;
    private string[] CharName = new string[6] { "nasu_left", "renkon_left", "kinoko_left", "nasu_right", "renkon_right", "kinoko_right" };


    [SerializeField] int OilFlyMinus = 500;

    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        SoundMan.Instance.PlayBGM("resultbgm",1.0f);

        //満足度の合計でランクを決定
        rank = RankManager.GetRank(GameDirector.totalSatisfyValue - (GameDirector.oilFlyHitNum * -1 * OilFlyMinus));

        var rankCtrl = GetComponent<RankController>("Rank");
        rankCtrl.DispRank = rank;


        //１日分の客の人数と決めたランクで売り上げの合計を算出
        money = SalesManager.CalcMoney(GameDirector.totalCustomerNum, rank);

        sceneFadeOut = GetComponent<SceneFadeOutSteam>("SceneFadeOutSteamManager");

        sceneFadeOut.IsStart = true;
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneFadeOut.IsFinish && !isStart)
        {
            InResult();
            SoundMan.Instance.PlaySE("cheer");
            isStart = true;
            var numberMan = GetComponent<Number_test>("NumberManager");
            numberMan.Init(money, new Vector3(0, 0, 0));
        }

        if (CharacterMoveFlg)
        {
            for (int i = 0; i < 3; i++)
            {
                Transform cameraTrans = GameObject.Find(CharName[i]).transform;
                Vector3 pos = cameraTrans.position;
                pos.x = pos.x + 0.1f;
                if (pos.x > CharaSpace*i-7.0f) pos.x = (float)(CharaSpace * i - 7.0f);
                cameraTrans.position = pos;
            }
            for (int i = 3; i < 6; i++)
            {
                Transform cameraTrans = GameObject.Find(CharName[i]).transform;
                Vector3 pos = cameraTrans.position;
                pos.x = pos.x - 0.1f;
                if (pos.x < CharaSpace * i- 3.0f) pos.x = (float)(CharaSpace * i - 3.0f);
                cameraTrans.position = pos;
            }
        }

        SoundMan.Instance.Update();
    }

    public void InResult()
    {
        StartCoroutine("InResultUpdate");
    }

    IEnumerator InResultUpdate()
    {
        CharacterMoveFlg = true;

        yield return null;
    }
}
