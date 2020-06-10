﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Scene = SatisfactionGauge;
using SoundMan = Singleton<SoundManager>;
using System;

public class NumCtrl_test : MonoBehaviour
{
    [SerializeField] private Sprite[] sp = new Sprite[10];

    private byte animecnt = 0;
    private Scene scene;

    private bool seflg;
    private bool delayFlg;
    private bool animeFlg;

    public bool LastAnimeFlg { get { return animeFlg; } }
    public void Start()
    {
        GameObject Satisfaction_gauge = GameObject.Find("Satisfaction_gauge");
        scene = Satisfaction_gauge.GetComponent<SatisfactionGauge>();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, 0);
        seflg = true;

    }
    public void ChangeSprite(int no)
    {

        if (no > 9 || no < 0) no = 0;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sp[no];
    }
    void Update()
    {


        if (scene.NextAnimeFlg && seflg)
        {
            SoundMan.Instance.PlaySE("register");
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            seflg = false;
            delayFlg = true;
        }
        if(delayFlg)
        {
            animecnt++;
            if (animecnt > 60)
            {
                animeFlg = true;
            }
        }
        
        SoundMan.Instance.Update();
    }

}