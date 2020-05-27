using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionGaugeController : BaseCompornent
{
    [SerializeField] Image obj = null;

    private const int GaugeMax = 3000;
    private const int GaugeMin = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        int value = Mathf.Clamp(GameDirector.totalSatisfyValue, GaugeMin, GaugeMax);
        obj.fillAmount = (float)value / GaugeMax;
        
    }
}
