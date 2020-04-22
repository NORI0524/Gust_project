using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : BaseCompornent
{
    private const float SteamMove = 0.005f;

    Transparent transparent;
    SpawnFactory fac;

    // Start is called before the first frame update
    void Start()
    {
        var anim = GetComponent<Animator>();
        anim.SetBool("steamFlag", true);

        transparent = GetComponent<Transparent>();

        var obj = GameObject.Find("SteamManager");
        fac = obj.GetComponent<SpawnFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        PosY += SteamMove;
        if (transparent.IsFinish())
        {
            fac.Decrease();
            Destroy(gameObject);
        }
    }
}
