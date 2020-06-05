using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public float fadeTime = 1.0f;
    private float currentRemainTime;
    private SpriteRenderer spRenderer;

    //アニメーター
    Animator animator = null;

    private void Start()
    {
        currentRemainTime = fadeTime;
        spRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        animator.SetBool("Start", false);
    }

    void Update()
    {
        animator.SetBool("Start", true);

        //アニメーションが開始されたらフェードアウト開始
        if (animator.GetBool("Start"))
        {
            //表示からフェードアウトまでの処理
            currentRemainTime -= Time.deltaTime;

            if (currentRemainTime <= 0.0f)
            {
                Animator.Destroy(gameObject);
                return;
            }

            //一定時間たったらアルファ値を下げる
            float alpha = currentRemainTime / fadeTime;
            var color = spRenderer.color;
            color.a = alpha;
            spRenderer.color = color;
        }
        
    }
}
