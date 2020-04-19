using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//温度調整管理システム
public class ThemoManager : BaseCompornent
{
    //範囲画像
    public Image obj; 

    const float MaxAngle = 359.9f;

    //温度と角度の比率
    private const float RateAngle = 180.0f / 80.0f;

    private float bestFirePowerMin;
    private float bestFirePowerMax;

    //温度の範囲幅
    private float range;
    private const float rangeInit = 60.0f;
    private const float rangeMax = 80.0f;

    //人数による増減
    private const float addRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        range = rangeInit;

        bestFirePowerMin = 60.0f;
        bestFirePowerMax = bestFirePowerMin + range;
    }

    // Update is called once per frame
    void Update()
    {
        //人数によって範囲を変更
        range = rangeInit - (GameDirector.bathingCustomerNum * addRange);

        //範囲に合わせて幅を変更
        obj.fillAmount = range / rangeMax;

        //現在の範囲
        float nowRange = bestFirePowerMax - bestFirePowerMin;

        //人数が増えた時
        if(nowRange > range)
        {
            //範囲の幅に合わせた温度設定に調整（ランダムで決める）
            float randomMax = bestFirePowerMax - range;
            bestFirePowerMin = Random.Range(bestFirePowerMin, randomMax);
        }
        //人数が減ったとき
        else if(nowRange < range)
        {
            float randomMin = bestFirePowerMax - range;
            bestFirePowerMin = Random.Range(randomMin, bestFirePowerMin);
        }
        //人数が変わらないとき
        else
        {

        }

        bestFirePowerMax = bestFirePowerMin + range;

        //範囲の回転
        float nowAngle = MaxAngle - (bestFirePowerMin * RateAngle);
        DegAngle = nowAngle - 90.0f;
    }

    public float GetBestFirePowerMin() { return bestFirePowerMin; }
    public float GetBestFirePowerMax() { return bestFirePowerMax; }

    public void OnGUI()
    {
        GUI.TextArea(new Rect(0, 250, 100, 50), "range : " + bestFirePowerMin.ToString("f2") + " ～ " + bestFirePowerMax.ToString("f2"));

    }
}
