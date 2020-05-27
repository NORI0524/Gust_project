using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public float fadeTime = 1.0f;
    private float currentRemainTime;
    private SpriteRenderer spRenderer;

    private void Start()
    {
        currentRemainTime = fadeTime;
        spRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //GetComponentを用いてAnimatorコンポーネントを取り出す.
        Animator animator = GetComponent<Animator>();

        //あらかじめ設定していたintパラメーター「trans」の値を取り出す.
        int start = animator.GetInteger("ReactionStart");

        //上矢印キーを押した際にパラメータ「trans」の値を増加させる.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            start++;
        }

        //下矢印キーを押した際にパラメータ「trans」の値を減少させる.
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            start--;
        }

        //intパラメーターの値を設定する.
        animator.SetInteger("ReactionStart", start);

        //表示からフェードアウトまでの処理
        currentRemainTime -= 0.1f;

        if (currentRemainTime <= 0.0f)
        {
            Animator.Destroy(gameObject);
            return;
        }

        //一定時間たったらアルファ値を下げる
        float alpha = currentRemainTime / fadeTime; ;
            var color = spRenderer.color;
            color.a = alpha;
            spRenderer.color = color;
        
    }
}
