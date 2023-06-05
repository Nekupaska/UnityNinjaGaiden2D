using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShurikenGroundState : PlayerBaseState
{
    float time;
    float speedOfClip = 1.5f;


    public override void EnterState(MainChar player)
    {
        //player.anim.CrossFade("wallrun", 0.15f);
        //player.anim.Play("wallrun");
        player.setInfoText("State: Shuriken Ground");

        time = 0;

        player.throwShuriken(player);


    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        time += Time.deltaTime;
        if (time > 0)
        //if (time > 0.6f)
        {
            player.TransitionToState(player.stateIdle);
        }



    }


}


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShurikenGroundState : PlayerBaseState
{
    float time;
    float speedOfClip = 1.5f;
    

    public override void EnterState(MainChar player)
    {
        //player.anim.CrossFade("wallrun", 0.15f);
        //player.anim.Play("wallrun");
        player.setInfoText("State: Shuriken Ground");

        time = 0;

        throwShuriken(player);


    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        time += Time.deltaTime;
        if (time > 0)
        //if (time > 0.6f)
        {
            player.TransitionToState(player.stateIdle);
        }



    }

    void throwShuriken(MainChar player)
    {
        //I THINK I SHOULD MAKE A SEPARATE SCRIPT
        //GameObject shu = Object.Instantiate(player.shuriken);
        GameObject shu = Object.Instantiate(player.shuriken, player.handPosition.position, Quaternion.identity);

        //player.prrint(shu.transform.position.x + " - " + shu.transform.position.y + " - " + shu.transform.position.z );

        //shu.transform.position = player.handPosition.position;

        GameObject enemy = getClosestEnemy(player);
        if (enemy != null)
        {
            //if there's enemy close to player and angle is in whatever range, throw towards their position

            shu.GetComponentInChildren<shurikenTrajectory>().isTracking = true;
            shu.GetComponentInChildren<shurikenTrajectory>().enemy = enemy;

        }
        else
        {
            //else throw straight forward until it hits something or timer == 1
            shu.GetComponentInChildren<shurikenTrajectory>().isTracking = false;
        }
        shu.GetComponentInChildren<shurikenTrajectory>().forward = player.right;
    }

    GameObject getClosestEnemy(MainChar player)
    {
        Collider[] objects = Physics.OverlapSphere(player.transform.position, player.trackRadius);
        GameObject en = null;

        foreach (var o in objects)
        {
            if (o.CompareTag("enemy"))
            {
                en = o.gameObject;
            }
        }

        return en;
    }

}

 
 */