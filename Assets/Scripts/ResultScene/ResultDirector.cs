using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rank = RankManager.Rank;

public class ResultDirector : BaseCompornent
{
    private int money;
    private Rank rank;

  

    // Start is called before the first frame update
    void Start()
    {
        GameDirector.totalSatisfyValue = 10000;

        //満足度の合計でランクを決定
        rank = RankManager.GetRank(GameDirector.totalSatisfyValue);

        //１日分の客の人数と決めたランクで売り上げの合計を算出
        money = SalesManager.CalcMoney(GameDirector.totalCustomerNum, rank);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(400, 350, 200, 100), "- Title -"))
        {
            SceneManager.LoadScene("TitleScene");
        }

        GUI.TextArea(new Rect(0, 150, 100, 50), "money : " + money);
    }
}
