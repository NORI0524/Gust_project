using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rank = RankManager.Rank;

public class ResultDirector : BaseCompornent
{
    private int money;
    private Rank rank;
    private bool CharacterMoveFlg = false;
    private string[] CharName = new string[6] { "nasu_left", "renkon_left", "kinoko_left", "nasu_right", "renkon_right", "kinoko_right" };

    // Start is called before the first frame update
    void Start()
    {
        GameDirector.totalSatisfyValue = 10000;

        //満足度の合計でランクを決定
        rank = RankManager.GetRank(GameDirector.totalSatisfyValue);

        //１日分の客の人数と決めたランクで売り上げの合計を算出
        //money = SalesManager.CalcMoney(GameDirector.totalCustomerNum, rank);
        money = 57687;

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

    public void OnGUI()
    {
        if (GUI.Button(new Rect(400, 350, 200, 100), "- Title -"))
        {
            SceneManager.LoadScene("TitleScene");
        }

        GUI.TextArea(new Rect(0, 150, 100, 50), "money : " + money);
    }
}
