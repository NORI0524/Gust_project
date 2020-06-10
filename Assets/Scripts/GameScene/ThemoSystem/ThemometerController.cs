using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//温度管理システム（温度計）
public class ThemometerController : BaseCompornent
{
    const float MinAngle = 50.0f;
    const float MaxAngle = 359.9f;

    public const float MinFirePower = 0.0f;
    public const float MaxFirePower = 140.0f;

    private float firePower;
    public const float InitFirePower = 80.0f;

    [SerializeField] float DecreacePower_Init = 2.0f;
    [SerializeField] float DecreacePowerRate_koromo = 1.0f;

    //天かすFac
    SpawnFactory koromoFac;

    //温度と角度の比率
    private const float RateAngle = 180.0f / 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        DegAngle = MinAngle;
        firePower = InitFirePower;

        var obj = GameObject.Find("KoromoManager");
        koromoFac = obj.GetComponent<SpawnFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        float decreace = DecreacePower_Init + koromoFac.GetCurrentNum() * DecreacePowerRate_koromo;
        firePower -= decreace / 60.0f;
        firePower = Mathf.Clamp(firePower, MinFirePower, MaxFirePower);
        float nowAngle = MaxAngle - (firePower * RateAngle);
        DegAngle = Mathf.Clamp(nowAngle, MinAngle, MaxAngle);
    }

    public void AddFirePower(float _add)
    {
        firePower += _add;
    }

    public float GetFirePower() { return firePower; }
}
