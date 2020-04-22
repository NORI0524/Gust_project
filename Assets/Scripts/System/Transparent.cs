using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : BaseCompornent
{
    Timer timer = null;
    private bool isFinish;
    private float frame;

    public float InitAlpha = 1.0f;
    public int TransparentSeconds = 5;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0.0f;
        isFinish = false;
        timer = new Timer(TransparentSeconds);
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
        frame += 1.0f / timer.GetToInitSeconds() / 60.0f;
        MaterialAlpha = Mathf.Lerp(InitAlpha, 0.0f, frame);
        isFinish = MaterialAlpha <= 0.0f; 
    }

    public bool IsFinish() { return isFinish; }
}
