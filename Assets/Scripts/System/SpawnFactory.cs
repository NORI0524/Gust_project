﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFactory : BaseCompornent
{
    //オブジェクトの種類
    public const int objTypeNum = 3;
    public GameObject[] obj_Prefab = new GameObject[objTypeNum];

    //１画面に表示する最大数
    public int ObjDispMax = 5;

    //現在のオブジェクト数
    private int currentNum;

    //１度にスポーンさせる数
    public int onceSpawnNum = 1;

    //スポーン頻度（確率：パーセント）
    public float spawnFrequency = 100.0f;

    //スポーンするまでのスパン
    public int spanSeconds = 2;
    Timer spanTimer;

    //スポーン用座標
    public float randX_Min;
    public float randX_Max;
    public float randY_Min;
    public float randY_Max;
    public float dispZ;

    // Start is called before the first frame update
    void Start()
    {
        spanTimer = new Timer(spanSeconds);
        spanTimer.Start();
        spanTimer.EnabledLoop();
        currentNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spanTimer.Update();
        if (!spanTimer.IsFinish()) return;
        if (currentNum >= ObjDispMax) return;

        //スポーンするかどうか
        float random = Random.Range(0.0f, 100.0f);
        if (random > spawnFrequency) return;

        //スポーン処理
        for(int cnt=0; cnt < onceSpawnNum; cnt++)
        {
            int select = Random.Range(0, objTypeNum);
            GameObject obj = Instantiate(obj_Prefab[select]) as GameObject;
            currentNum++;

            float px = Random.Range(randX_Min, randX_Max);
            float py = Random.Range(randY_Min, randY_Max);
            obj.transform.position = new Vector3(px, py, dispZ);
        }
    }

    public int GetCurrentNum() { return currentNum; }
    public void Decrease() { currentNum--; }

    //設定関係
    public void SetSpawnFrequency(float value)
    {
        spawnFrequency = Mathf.Clamp(value, 0.0f, 100.0f);
    }
    public void SetSpawnSeconds(int value)
    {
        spanSeconds = Mathf.Max(value, 0);
    }
    public void SetOnceSpawnNum(int value)
    {
        onceSpawnNum = Mathf.Max(value, 1);
    }
}
