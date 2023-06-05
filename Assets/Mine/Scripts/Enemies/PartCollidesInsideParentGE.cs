using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCollidesInsideParentGE : MonoBehaviour
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
            parent.GetComponent<AIGroundEnemy>().player = obj;
            parent.GetComponent<AIGroundEnemy>().isColliding = true;
            //obj.GetComponent<MainChar>().getHit();
        }
    }

    void OnTriggerExit(Collider col)
    {
        var obj = col.gameObject;

        if (obj.CompareTag("player"))
        {
            parent.GetComponent<AIGroundEnemy>().player = null;
            parent.GetComponent<AIGroundEnemy>().isColliding = false;
        }
    }
}
