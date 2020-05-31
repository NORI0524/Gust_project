using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rank = RankManager.Rank;

public class ResultDirector : BaseCompornent
{
    private int money;
    private Rank rank;
    private int minus;
    private int missTimes = 0;
    public Rank RankResult { get { return rank; } }
    private bool CharacterMoveFlg = false;
    private string[] CharName = new string[6] { "nasu_left", "renkon_left", "kinoko_left", "nasu_right", "renkon_right", "kinoko_right" };

    // Start is called before the first frame update
    void Start()
    {
        GameDirector.totalSatisfyValue = 10000;

        minus = -500;

        //missTimes=GameDirector     ここにミスの回数入れてね!

        //満足度の合計でランクを決定
        rank = RankManager.GetRank(GameDirector.totalSatisfyValue - (missTimes * minus));


        //１日分の客の人数と決めたランクで売り上げの合計を算出
        money = SalesManager.CalcMoney(GameDirector.totalCustomerNum, rank);


        var numberMan = GetComponent<Number_test>("NumberManager");
        numberMan.Init(money, new Vector3(0, 0, 0));
        

        InResult();
    }

    // Update is called once per frame
    void Update()
    {
       if(CharacterMoveFlg==true)
        {
            for (int i = 0; i < 3; i++)
            {
                Transform cameraTrans = GameObject.Find(CharName[i]).transform;
                Vector3 pos = cameraTrans.position;
                pos.x = pos.x + 0.1f;
                if (pos.x > 3*i-7.5) pos.x = (float)(3 * i - 7.5);
                cameraTrans.position = pos;
            }
            for (int i = 3; i < 6; i++)
            {
                Transform cameraTrans = GameObject.Find(CharName[i]).transform;
                Vector3 pos = cameraTrans.position;
                pos.x = pos.x - 0.1f;
                if (pos.x < 3 * i- 7.5) pos.x = (float)(3 * i - 7.5);
                cameraTrans.position = pos;
            }
        }
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
