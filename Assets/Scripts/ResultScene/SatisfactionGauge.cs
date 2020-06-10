using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene = SceneFadeOutSteam;
using SoundMan = Singleton<SoundManager>;

public class SatisfactionGauge : BaseCompornent
{
    [SerializeField] Image obj = null;
    [SerializeField] float GaugeSpeed = 0.1f;

    private Scene scene;



    private const int GaugeMax = 3000;
    private const int GaugeMin = 0;
    private bool animeFlg;
    private int value;

    public bool NextAnimeFlg { get { return animeFlg; } }
    // Start is called before the first frame update
    void Start()
    {
        GameObject SceneFadeOutSteamManager = GameObject.Find("SceneFadeOutSteamManager");
        scene = SceneFadeOutSteamManager.GetComponent<SceneFadeOutSteam>();
        value = Mathf.Clamp(GameDirector.totalSatisfyValue, GaugeMin, GaugeMax);
        obj.fillAmount = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {

        if (scene.IsFinish)
        {
            //サウンド再生
            //SoundMan.Instance.PlaySE("startdrumroll");
            obj.fillAmount = Mathf.Clamp(obj.fillAmount + GaugeSpeed, GaugeMin, (float)value / GaugeMax);


            if (obj.fillAmount >= (float)value / GaugeMax)
            {
                animeFlg = true;
            }
        }

        SoundMan.Instance.Update();
    }
}
