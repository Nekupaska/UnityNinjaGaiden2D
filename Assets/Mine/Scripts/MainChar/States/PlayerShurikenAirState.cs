using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShurikenAirState : PlayerBaseState
{
    float time;
    float speedOfClip = 0.5f;
    int timesThrown = 0;

    public override void EnterState(MainChar player)
    {
        //player.anim.CrossFade("wallrun", 0.15f);
        player.anim.Play("FallingThrowing");
        player.setInfoText("State: Shuriken Air");

        time = 0;

        player.velocity.y = player.jumpSpeed * 1f;

        player.throwShuriken(player);
        timesThrown = 1;
        player.canThrowAirShuriken = false;

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }


    Vector3 v;

    public override void Update(MainChar player)
    {
        //player.fall();
        //throw timings: 0, 0.05,0.15,0.21

        time += Time.deltaTime;
        v = player.fallVector(); //player.fall();
        //v.z = player.moveFowardVector(-player.jumpSteerSpeed * 2).z;

        if (time >= 0.05f * speedCalc() && time < 0.15f * speedCalc() && timesThrown == 1)
        {
            timesThrown = 2;
            player.throwShuriken(player);
        }

        if (time >= 0.15f * speedCalc() && time < 0.21f * speedCalc() && timesThrown == 2)
        {
            timesThrown = 3;
            player.throwShuriken(player);
        }

        player.move(v);

        if (time > 0.21f * speedCalc())
        {
            //throw last shuriken
            player.throwShuriken(player);
            player.TransitionToState(player.stateFalling);
        }



    }

    float speedCalc()
    {
        return 1 / speedOfClip;
    }



}


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShurikenAirState : PlayerBaseState
{
    float time;
    float speedOfClip = 0.5f;
    int timesThrown = 0;

    public override void EnterState(MainChar player)
    {
        //player.anim.CrossFade("wallrun", 0.15f);
        player.anim.Play("FallingThrowing");
        player.setInfoText("State: Shuriken Air");

        time = 0;

        player.velocity.y = player.jumpSpeed * 1f;

        throwShuriken(player);
        timesThrown = 1;
        player.canThrowAirShuriken = false;

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }


    Vector3 v;

    public override void Update(MainChar player)
    {
        //player.fall();
        //throw timings: 0, 0.05,0.15,0.21

        time += Time.deltaTime;
        v = player.fallVector(); //player.fall();
        //v.z = player.moveFowardVector(-player.jumpSteerSpeed * 2).z;

        if (time >= 0.05f * speedCalc() && time < 0.15f * speedCalc() && timesThrown == 1)
        {
            timesThrown = 2;
            throwShuriken(player);
        }

        if (time >= 0.15f * speedCalc() && time < 0.21f * speedCalc() && timesThrown == 2)
        {
            timesThrown = 3;
            throwShuriken(player);
        }

        player.move(v);

        if (time > 0.21f * speedCalc())
        {
            //throw last shuriken
            throwShuriken(player);
            player.TransitionToState(player.stateFalling);
        }



    }

    float speedCalc()
    {
        return 1 / speedOfClip;
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