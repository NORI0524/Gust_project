using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//子オブジェクトの当たり判定をを親に渡す用
public class ChildColliderTrigger : BaseCompornent
{
    BaseCharactorController parent;

    // Start is called before the first frame update
    void Start()
    {
       GameObject objParent = gameObject.transform.parent.gameObject;
       parent = objParent.GetComponent<BaseCharactorController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        parent.RelayOnTriggerStay2D(collision);
    }
}
