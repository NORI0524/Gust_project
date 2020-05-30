using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucePanManager : BaseCompornent
{

    [SerializeField] GameObject prefab = null;
    bool isAlive;

    public bool IsAlive { get { return isAlive; } }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (isAlive)
        {
            var obj = GameObject.Find("SaucePan_Lib");
            Destroy(obj);
            isAlive = false;
        }
        else
        {
            var obj = Instantiate(prefab) as GameObject;
            obj.name = "SaucePan_Lib";
            isAlive = true;
        }  
    }
}
