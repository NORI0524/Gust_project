using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fan : BaseCompornent
{
    public const float AddFirePower = 2.0f;
    private Animator animator;
    GameObject themo;
    ThemometerController themoCtrl;


    BitFlag flg = new BitFlag();
    class FanState
    {
        public const uint None = 0;
        public const uint Touch = 1 << 1;

    }

    // Start is called before the first frame update
    void Start()
    {
        flg.FoldALLBit();
        themo = GameObject.Find("Themometer_main");
        themoCtrl = themo.GetComponent<ThemometerController>();
        animator = GetComponent<Animator>();
        animator.SetBool("FanFlag", false);
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime < 1.0f)
        {
            animator.SetBool("FanFlag", false);
        }
    }

    public void Touch()
    {
        var obj = GameObject.Find("SaucePan_Lib");
        if (obj != null) return;

        animator.SetBool("FanFlag", true);
        themoCtrl.AddFirePower(AddFirePower);
    }

}
