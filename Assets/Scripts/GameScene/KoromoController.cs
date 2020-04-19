using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoromoController : BaseCompornent
{
    bool isCatch = false;
    bool isTouch = false;
    int VibrationCount = 15;        //振動の間隔
    float VibrationWidth = 0.1f;    //振動の幅
    float MoveX = 0.0f;
    float MoveY = 0.0f;
    float PopWide = 0.7f;           //衣タッチ時の飛び散る幅
    float PopMaxSpeed = 1.0f;       //衣タッチ時の最高速度
    float PopMinSpeed = 0.5f;       //衣タッチ時の最低速度(-にすると下にも飛ぶ)

    SpawnFactory koromoFac;

    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.Find("KoromoFactory");
        koromoFac = obj.GetComponent<SpawnFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            MoveX = Random.Range(-PopWide, PopWide);
            MoveY = Random.Range(PopMinSpeed, PopMaxSpeed);
            isCatch = true;
            isTouch = false;
        }
        else
        {
            VibrationCount -= 1;

            if (VibrationCount <= 0)
            {
                float px = Random.Range(-VibrationWidth, VibrationWidth);
                AddPosition(px, 0, 0);
                VibrationCount = 15;
            }
        }

        if (isCatch)
        {
            AddPosition(MoveX, MoveY, 0);
        }
        if (PosY > 500)
        {
            koromoFac.Decrease();
            Destroy(gameObject);
        }
    }

    public void OnClick()
    {
        isTouch = true;
        Debug.Log("クリック");
    }
}

//スポーン座標関係　メモ

//float px = Random.Range(-2.7f, 3.1f);
//float py = Random.Range(-1.3f, 0.3f);
//var collider = Physics2D.OverlapCircle(new Vector2(px, py), 0.35f);

//        if (collider.CompareTag("Bathtub"))
//        {
//            GameObject obj = Instantiate(Koromo_Prefab) as GameObject;

//obj.transform.position = new Vector3(px, py, -1);
