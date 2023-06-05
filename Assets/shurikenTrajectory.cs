using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenTrajectory : MonoBehaviour
{
    public bool isTracking = false;
    public bool forward = true;
    private float time = 0;
    private bool moving = true;
    public GameObject enemy;
    Vector3 trajectory;

    void Start()
    {
        GetComponentInChildren<Animator>().Play("spin");
        time = 0;

        if (isTracking)
        {
            //if it hits and enemy, it dissapears 
            //else if it hits something,
            trajectory = transform.position - enemy.transform.position;

            int sign = forward ? 1 : -1;
            sign = -1;
            //GetComponent<Rigidbody>().AddForce(trajectory.normalized * 750 * sign, ForceMode.Force);
            GetComponent<Rigidbody>().AddForce(trajectory.normalized * 1000 * sign, ForceMode.Force);

        }
        else
        {
            if (forward)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.forward * 750, ForceMode.Force);
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(Vector3.back * 750, ForceMode.Force);

            }
        }
    }

    void Update()
    {

        time += Time.deltaTime;
        if (time > 1.5f)
        {
            Destroy(this.gameObject); //dispawns after sometime in the air
        }


        if (moving)
        {

        }
    }


    void OnTriggerEnter(Collider col)
    {
        var obj = col.gameObject;
        if (col.tag.Contains("enemy"))
        {
            //hit and dispawn
            //damage
            if (obj.tag.ToLower().Contains("flying"))
            {
                if (obj.GetComponentInParent<AIFlyingEnemy>() != null)
                    obj.GetComponentInParent<AIFlyingEnemy>().getHit(false);
            }
            else if (obj.name.ToLower().Contains("ground"))
            {
                if (obj.GetComponentInParent<AIGroundEnemy>() != null)
                    obj.GetComponentInParent<AIGroundEnemy>().getHit(false,5);
            }
            Destroy(this.gameObject);

        }
        //else if (!col.CompareTag("player") && !col.gameObject.name.Contains("shuriken"))
        else if (obj.layer == LayerMask.NameToLayer("Level"))
        {
            time = 0; //reset timer, disappears after some time in Update()
            moving = false; // it gets stuck in geometry
            GetComponentInChildren<Animator>().speed = 0;
            GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    /*public void launchAttack(Collider col)
    {
        //col is the attackBox of the player, c is the hitbox of the enemy
        Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("enemy"));

        foreach (Collider c in colliders)
        {
            var obj = c.gameObject;
            if (obj.CompareTag("enemy")) //if it's an enemy
            {

                if (obj.gameObject.name.Contains("flyingEnemy"))
                {
                    obj.GetComponentInParent<AIFlyingEnemy>().transitionState(AIFlyingEnemy.State.stagger);
                }
                else if (obj.gameObject.name.Contains("groundEnemy"))
                {
                    ///if(in the air) juggle
                    ///else () stagger
                }
            }
        }

    }*/

}
