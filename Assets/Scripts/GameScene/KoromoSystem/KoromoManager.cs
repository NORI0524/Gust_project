using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoromoManager : BaseCompornent
{
    SpawnFactory koromoFac;

    private const float AddFrequency = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        koromoFac = this.GetComponent<SpawnFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        //入浴してるならスポーン起動
        if(GameDirector.bathingCustomerNum > 0)
        {
            koromoFac.OnActive();
        }
        else
        {
            koromoFac.OffActive();
        }

        if (GameDirector.isFinish) koromoFac.OffActive();

        //TODO:入浴中のお客さんの数でスポーンの設定を変えるかも
        koromoFac.spawnFrequency = GameDirector.bathingCustomerNum * AddFrequency;
    }
}
