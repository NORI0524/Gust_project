using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSteam : BaseCompornent
{
    private const float SteamMove = 0.02f;

    Transparent transparent;
    
    void Start()
    {
        transparent = GetComponent<Transparent>();
    }

    void Update()
    {
        PosY += SteamMove;
        if (PosY >= 3)
        {
            Destroy(gameObject);
        }
    }
}
