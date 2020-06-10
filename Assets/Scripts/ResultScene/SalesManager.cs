using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rank = RankManager.Rank;

public class SalesManager
{
    private static Dictionary<Rank, int> rankBonusDic = new Dictionary<Rank, int>()
    {
        {Rank.Beginner,500},
        {Rank.Bronze,1000},
        {Rank.Iron,5000 },
        {Rank.Silver,10000},
        {Rank.Gold,15000 },
        {Rank.Ruby,20000 },
        {Rank.Platinum,25000 },
        {Rank.Diamond,35000 },
        {Rank.Master,40000 },
        {Rank.Predator,50000 }
    };

    public static int CalcMoney(int value, Rank rank)
    {
        int money = 500;
        money = money * value + rankBonusDic[rank];
        Debug.Log(value);
        Debug.Log(rankBonusDic[rank]);
        return money;
    }

    public static int GetMoney()
    {
        return PlayerPrefs.GetInt("SCORE", 0);
    }


    public static void AddTotalMoney(int add)
    {
        int total = PlayerPrefs.GetInt("SCORE",0);
        total = total + add;
        PlayerPrefs.SetInt("SCORE", total);
        PlayerPrefs.Save();
    }
}
