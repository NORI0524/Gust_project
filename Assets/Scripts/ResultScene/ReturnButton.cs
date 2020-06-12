using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class ReturnButton : BaseCompornent
{
    FadeValue fade = new FadeValue(1.0f, 1, 1.0f, 1.15f);

    private bool scoredeleteFlg;
    public bool deleteFlg { get { return scoredeleteFlg; } }

    SceneFadeInSteam sceneFadeIn;

    // Start is called before the first frame update
    void Start()
    {
        sceneFadeIn = GetComponent<SceneFadeInSteam>("SceneFadeInSteamManager");
    }

    // Update is called once per frame
    void Update()
    {
        fade.Update();
        ScaleX = ScaleY = fade.GetCurrentValue();

        if (sceneFadeIn.IsFinish)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void Click()
    {
        scoredeleteFlg = true;
        SoundMan.Instance.PlaySE("tap");
        sceneFadeIn.IsStart = true;
    }
}
