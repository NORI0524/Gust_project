using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : BaseCompornent
{
    private const float addMove = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PosX += addMove;
    }
}
