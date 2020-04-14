using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : BaseCompornent
{
    private const float SteamMove = 0.005f;

    float Alpha = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        var anim = GetComponent<Animator>();
        anim.SetBool("steamFlag", true);
    }

    // Update is called once per frame
    void Update()
    {
        PosY += SteamMove;

        MaterialAlpha = Alpha;
        Alpha -= 0.001f;

        if(Alpha < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
