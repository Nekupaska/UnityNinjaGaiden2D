using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCollidesInsideParentFE : MonoBehaviour
{
    public GameObject parent;


    //void Start()
    //{

    //}

    //void Update()
    //{

    //}

    void OnTriggerEnter(Collider col)
    {
        var obj = col.gameObject;
        if (obj.CompareTag("player"))
        {
            parent.GetComponent<AIFlyingEnemy>().player = obj;
            parent.GetComponent<AIFlyingEnemy>().isColliding = true;
            //obj.GetComponent<MainChar>().getHit();
        }
    }

    void OnTriggerExit(Collider col)
    {
        var obj = col.gameObject;

        if (obj.CompareTag("player"))
        {
            parent.GetComponent<AIFlyingEnemy>().player = null;
            parent.GetComponent<AIFlyingEnemy>().isColliding = false;
        }
    }
}
