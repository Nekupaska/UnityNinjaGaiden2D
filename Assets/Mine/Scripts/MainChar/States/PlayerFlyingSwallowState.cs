using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyingSwallowState : PlayerBaseState
{
    float time;
    float speedOfClip = 2;
    bool trackMode = false;
    bool ableToTrack = true; //debug
    GameObject enemy;
    float angle;
    bool alreadyHit = false;


    public override void EnterState(MainChar player)
    {
        trackMode = false;
        enemy = null;
        alreadyHit = false;

        player.anim.CrossFade("flyingSwallow", 0.15f);
        player.setInfoText("State: Flying Swallow");

        player.playClip(player.twoah);
        time = 0;

        /*THIS DOESN'T WORK, IGNORE*/
        //enemy = player.getClosestEnemy(player);
        //if (enemy != null)
        //{
        //    v = player.transform.position - enemy.transform.position;
        //    v = v.normalized * player.moveSpeed * -3f;
        //    //angle = Vector3.SignedAngle(player.transform.forward, v, player.transform.right) * 180 / Mathf.PI; //radians to degrees
        //    angle = Vector3.Angle(player.handPosition.forward, -v);
        //    angle = angle > 180 ? angle - 180 : angle; //not interested in over rotations
        //    //if (angle <= 25 && angle >= -25) //between 25 and -25
        //    if (angle <= 0.5f && angle >= 5.9) //between 25 and -25 degrees
        //    {
        //        trackMode = true;
        //    }
        //}

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    Vector3 v;
    public override void Update(MainChar player)
    {
        time += Time.deltaTime;

        if (ableToTrack && trackMode)
        {
            player.move(v);
        }
        else
        {
            player.moveFoward(player.moveSpeed * 3f);
        }

        if (!alreadyHit && player.launchAttack(player.attackColliders[1], false,20))
        {
            alreadyHit = true;
        }

        //player.updateRotation();
        if (SimpInput.getIfHeld(new string[] { "t" }))
        {
            //flying swallow
            //player.TransitionToState(player.stateFlyingSwallow);
        }

        //if (player.isAnimFree("flyingSwallow"))
        if (time >= player.clips["fk"].length / speedOfClip)
        {
            player.snapToGround = true;

            player.TransitionToState(player.stateIdle);
        }
    }

}
