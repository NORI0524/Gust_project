using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class TitleDirector : BaseCompornent
{
    SceneFadeInSteam sceneFadeIn;
    SceneFadeOutSteam sceneFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        SoundMan.Instance.PlayBGM("titlebgm", 1.0f);
        sceneFadeIn = GetComponent<SceneFadeInSteam>("SceneFadeInSteamManager");
        sceneFadeOut = GetComponent<SceneFadeOutSteam>("SceneFadeOutSteamManager");
        sceneFadeOut.IsStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        SoundMan.Instance.Update();


        if (sceneFadeIn.IsFinish)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

