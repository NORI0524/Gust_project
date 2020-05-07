using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{

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
    }
}
