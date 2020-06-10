
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Scene = SatisfactionGauge;
public class NumCtrl_test : MonoBehaviour
{
    [SerializeField] private Sprite[] sp = new Sprite[10];

    private byte anime = 0;
    private Scene scene;

    private bool animeFlg;

    public bool LastAnimeFlg { get { return animeFlg; } }
    public void Start()
    {
        GameObject Satisfaction_gauge = GameObject.Find("Satisfaction_gauge");
        scene = Satisfaction_gauge.GetComponent<SatisfactionGauge>();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, 0);

    }
    public void ChangeSprite(int no)
    {

        if (no > 9 || no < 0) no = 0;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sp[no];
    }
    void Update()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32(0, 0, 0, anime);

        if (scene.NextAnimeFlg)
        {
            if (anime < 255)
            {
                anime += 3;
            }
            else
            {
                anime = 255;
                animeFlg = true;
            }
        }

    }

}