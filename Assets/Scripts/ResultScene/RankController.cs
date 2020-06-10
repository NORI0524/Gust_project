using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Rank = RankManager.Rank;
using SoundMan = Singleton<SoundManager>;

public class RankController : BaseCompornent
{
    private Rank rank;

    private NumCtrl_test numCtrl;

    private bool seflg;

    private bool particleFlg;

    SpriteRenderer spriteRenderer = null;

    public bool ParticleFlg { get { return particleFlg; } }

    [SerializeField] Sprite[] Ranksprite=new Sprite[10];

    public Rank DispRank { get { return rank; } set { rank = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //数字のフラグを取得するため
        GameObject pref_num_test = GameObject.Find("pref_num_test");
        numCtrl = pref_num_test.GetComponent<NumCtrl_test>();


        //スプライトレンダー
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, 0);
        seflg = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (numCtrl.LastAnimeFlg && !seflg)
        {
            SoundMan.Instance.PlaySE("stamp");

            spriteRenderer.color = new Color32(255, 255, 255, 255);
            seflg = true;
            particleFlg = true;
        }


        //ランクごとに画像を変更
        if (rank == Rank.Beginner)
        {
            spriteRenderer.sprite = Ranksprite[0];
        }
        if (rank == Rank.Bronze)
        {
            spriteRenderer.sprite = Ranksprite[1];
        }
        if (rank == Rank.Iron)
        {
            spriteRenderer.sprite = Ranksprite[2];
        }
        if (rank == Rank.Silver)
        {
            spriteRenderer.sprite = Ranksprite[3];
        }
        if (rank == Rank.Gold)
        {
            spriteRenderer.sprite = Ranksprite[4];
        }
        if (rank == Rank.Ruby)
        {
            spriteRenderer.sprite = Ranksprite[5];
        }
        if (rank == Rank.Platinum)
        {
            spriteRenderer.sprite = Ranksprite[6];
        }
        if (rank == Rank.Diamond)
        {
            spriteRenderer.sprite = Ranksprite[7];
        }
        if (rank == Rank.Master)
        {
            spriteRenderer.sprite = Ranksprite[8];
        }
        if (rank == Rank.Predator)
        {
            spriteRenderer.sprite = Ranksprite[9];
        }

        SoundMan.Instance.Update();
    }
}