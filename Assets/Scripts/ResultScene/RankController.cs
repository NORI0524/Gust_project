using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Rank = RankManager.Rank;
using Scene = NumCtrl_test;
using SoundMan = Singleton<SoundManager>;

public class RankController : BaseCompornent
{
    private Rank rank;

    private Scene scene;

    private bool seflg;

    [SerializeField] Sprite[] Ranksprite=new Sprite[10];
    SpriteRenderer spriterenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        var Result = GetComponent<ResultDirector>("ResultDirector");
        GameObject pref_num_test = GameObject.Find("pref_num_test");
        scene = pref_num_test.GetComponent<NumCtrl_test>();
        rank = Result.RankResult;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, 0);
        seflg = true;
    }
    
    
    
    // Update is called once per frame
    void Update()
    {

        if(scene.LastAnimeFlg&&seflg)
        {
            SoundMan.Instance.PlaySE("stamp");

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color32(255, 255, 255, 255);
            seflg = false;
        }
       
        if (rank == Rank.Beginner)
        {
            spriterenderer.sprite = Ranksprite[0];
        }
        if (rank == Rank.Bronze)
        {
            spriterenderer.sprite = Ranksprite[1];
        }
        if (rank == Rank.Iron)
        {
            spriterenderer.sprite = Ranksprite[2];
        }
        if (rank == Rank.Silver)
        {
            spriterenderer.sprite = Ranksprite[3];
        }
        if (rank == Rank.Gold)
        {
            spriterenderer.sprite = Ranksprite[4];
        }
        if (rank == Rank.Ruby)
        {
            spriterenderer.sprite = Ranksprite[5];
        }
        if (rank == Rank.Platinum)
        {
            spriterenderer.sprite = Ranksprite[6];
        }
        if (rank == Rank.Diamond)
        {
            spriterenderer.sprite = Ranksprite[7];
        }
        if (rank == Rank.Master)
        {
            spriterenderer.sprite = Ranksprite[8];
        }
        if (rank == Rank.Predator)
        {
            spriterenderer.sprite = Ranksprite[9];
        }

        SoundMan.Instance.Update();
    }
}
