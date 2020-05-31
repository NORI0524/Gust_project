using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundMan = Singleton<SoundManager>;

public class StartButton : BaseCompornent
{
    FadeValue fade = new FadeValue(1.0f, 1, 1.0f, 1.15f);

    Animator anim;

    Timer startSETimer = new Timer(2);

    bool startSEflg = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>("Shishiodoshi");
        if (anim == null)
        {
            Debug.LogError("Error");
        }
        anim.SetBool("Play", false);

        
    }

    // Update is called once per frame
    void Update()
    {
        fade.Update();
        ScaleX = ScaleY = fade.GetCurrentValue();

        startSETimer.Update();

       
        //ししおどし終了
        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime < 1.0f)
        {
            anim.SetBool("Play", false);

            if (startSEflg)
            {
                var sceneFadeIn = GetComponent<SceneFadeInSteam>("SceneFadeSteamManager");
                sceneFadeIn.IsStart = true;
            }
        }

        //ししおどしタイミング調整
        if (startSEflg == false && startSETimer.IsFinish())
        {
            SoundMan.Instance.PlaySE("shishiodoshi"); 
            startSEflg = true;
        }
    }

    public void Click()
    {
        SoundMan.Instance.PlaySE("tap");

        anim.SetBool("Play", true);
        startSETimer.Start();
    }
}