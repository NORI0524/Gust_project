using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeOutSteam : BaseCompornent
{
    [SerializeField] GameObject prefab = null;

    [SerializeField] int SteamNum = 15;

    int nowCount;
    bool isStart;
    bool isFinish;

    int frame = 0;
    [SerializeField] int FadeFrame = 15;

    public bool IsStart { set { isStart = value; } }
    public bool IsFinish { get { return isFinish; } }

    // Start is called before the first frame update
    void Start()
    {
        for(nowCount = 1; nowCount <= SteamNum; nowCount++)
        {
            //湯気生成
            GameObject obj = Instantiate(prefab) as GameObject;
            obj.transform.position = new Vector3(0, 0, 0);
            obj.name = "SceneSteam_" + nowCount;
        }
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        frame++;
        if (frame > FadeFrame)
        {
            frame = 0;
            Destroy("SceneSteam_" + nowCount);
            nowCount--;
        }

        if (nowCount == 0)
        {
            isFinish = true;
            Destroy(gameObject);
        }
    }
}
