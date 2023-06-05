using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform focus;
    public float heightOffset;
    void Start()
    {
        heightOffset = this.transform.position.y;

    }

    void Update()
    {
        float depth = this.transform.position.x;
        

        Vector3 newPosition = new Vector3(depth, focus.position.y + heightOffset, focus.position.z);

        this.transform.position = newPosition;

    }
}
