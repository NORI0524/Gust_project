using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFlyManager : MonoBehaviour
{
    SpawnFactory oilFac;

    // Start is called before the first frame update
    void Start()
    {
        oilFac = GetComponent<SpawnFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        //入浴してるならスポーン起動
        if (GameDirector.bathingCustomerNum > 0)
        {
            oilFac.OnActive();
        }
        else
        {
            oilFac.OffActive();
        }

        if (GameDirector.isFinish) oilFac.OffActive();
    }
}
