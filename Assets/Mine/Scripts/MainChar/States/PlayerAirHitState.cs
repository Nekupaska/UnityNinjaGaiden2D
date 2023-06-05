using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirHitState : PlayerBaseState
{
    float time;
    float speedOfClip = 1;

    public override void EnterState(MainChar player)
    {
        player.anim.Play("fallingHit");
        player.setInfoText("State: Midair hit");

        time = 0;
        player.velocity = Vector3.up * player.jumpSpeed;
        player.snapToGround = false;
    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    Vector3 v;
    public override void Update(MainChar player)
    {
        time += Time.deltaTime;

        v = player.fallVector()*2;
        player.con.Move(v);

        if (player.isGrounded())
        {
            //player.snapToGround = true;

            player.TransitionToState(player.stateSplatHit);
        }
    }

}
