using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class TitleDirector : BaseCompornent
{
    SceneFadeInSteam sceneFade;

    // Start is called before the first frame update
    void Start()
    {
        SoundMan.Instance.PlayBGM("titlebgm", 1.0f);
        sceneFade = GetComponent<SceneFadeInSteam>("SceneFadeSteamManager");
    }

    // Update is called once per frame
    void Update()
    {
        SoundMan.Instance.Update();


        if (sceneFade.IsFinish)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

