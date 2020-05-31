using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundMan = Singleton<SoundManager>;

public class TitleDirector : BaseCompornent
{ 
    // Start is called before the first frame update
    void Start()
    {
        SoundMan.Instance.PlayBGM("titlebgm", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        SoundMan.Instance.Update();
    }


    public void OnGUI()
    {
       
    }
}
