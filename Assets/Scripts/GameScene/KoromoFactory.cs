using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoromoFactory : MonoBehaviour
{
    public GameObject Koromo_Prefab;

    //衣がスポーンされる頻度（確率：パーセント）
    public const float SpawnFrequency = 100.0f;

    //衣がスポーンされるまでのスパン
    public const int koromoSpanSeconds = 3;
    Timer spanTimer = new Timer(koromoSpanSeconds);


    // Start is called before the first frame update
    void Start()
    {
        spanTimer.Start();
        spanTimer.EnabledLoop();
    }

    // Update is called once per frame
    void Update()
    {
        spanTimer.Update();
        if (!spanTimer.IsFinish()) return;

        float px = Random.Range(-2.7f, 3.1f);
        float py = Random.Range(-1.3f, 0.3f);
        var collider = Physics2D.OverlapCircle(new Vector2(px, py), 0.35f);

        if (collider.CompareTag("Bathtub"))
        {
            GameObject obj = Instantiate(Koromo_Prefab) as GameObject;

            obj.transform.position = new Vector3(px, py, -1);
        }

    }
}