using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class StartButton : BaseCompornent
{
    FadeValue fade = new FadeValue(1.0f, 2, 0.5f, 1.5f);

    Animator anim;

    Timer startSETimer = new Timer(2);

    bool startSEflg = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>("Shishiodoshi");
        if (anim == null)
        {
            Debug.LogError("ぴえん");
        }
        anim.SetBool("Play", false);

        
    }

    // Update is called once per frame
    void Update()
    {
        fade.Update();
        ScaleX = fade.GetCurrentValue();

        startSETimer.Update();

       

        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime < 1.0f)
        {
            
            anim.SetBool("Play", false);
            
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
        //SceneManager.LoadScene("GameScene");
        SoundMan.Instance.PlaySE("tap");

        anim.SetBool("Play", true);
        startSETimer.Start();
    }

    public void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 100, 50), "scaleX " + ScaleX);
    }
}