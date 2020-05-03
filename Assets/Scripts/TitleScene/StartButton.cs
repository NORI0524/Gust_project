using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : BaseCompornent
{
    FadeValue fade = new FadeValue(1.0f, 2, 0.5f, 2.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fade.Update();
        ScaleX = fade.GetCurrentValue();
    }

    public void Click()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 100, 50), "scaleX " + ScaleX);
    }
}