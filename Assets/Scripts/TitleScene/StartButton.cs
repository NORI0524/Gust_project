using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : BaseCompornent
{
    FadeValue fade = new FadeValue(1.0f, 2, 0.5f, 1.5f);

    Animator anim;

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

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Play", true);
        }

        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime < 1.0f)
        {
            anim.SetBool("Play", false);
        }
    }

    public void Click()
    {
        //SceneManager.LoadScene("GameScene");
    }

    public void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 100, 50), "scaleX " + ScaleX);
    }
}