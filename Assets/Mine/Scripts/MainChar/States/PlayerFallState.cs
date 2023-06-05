using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    float time;
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("fall", 0.15f);
        //player.prrint("idle");
        player.setInfoText("State: Falling");
        time = 0;
        player.snapToGround = false;
    }

    public override void OnCollisionEnter(MainChar player)
    {
    }


    Vector3 v;
    public override void Update(MainChar player)
    {
        //player.fall();

        time += Time.deltaTime;

        v = player.fallVector();

        if (SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" }))
        {
            player.updateRotation();
            v.z = player.moveFowardVector(player.jumpSteerSpeed).z; //player.moveFoward(player.jumpSteerSpeed);
        }

        player.move(v);

        if (player.isGrounded())
        {
            player.snapToGround = true;

            player.TransitionToState(player.stateIdle);

        }
        else
        {
            //maybe I should separate this into another state, falling state, then encapsulate it. Then from the falling state, if grounded, go back to idle

            if (SimpInput.getIfPressed(new string[] { "c" }) && player.canThrowAirShuriken)
            {
                player.TransitionToState(player.stateShurikenAir);
            }
        }

        

        


    }
}


/*
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("idle", 0.15f);
        //player.prrint("idle");
        player.setInfoText("State: Idle");
    }

    public override void OnCollisionEnter(MainChar player)
    {
    }

    public override void Update(MainChar player)
    {
        player.fall();

        if (SimpInput.getIfPressed(new string[] { "x" }))
        {
            //normal jump
            player.TransitionToState(player.stateJumping);
        }

        if (SimpInput.getIfHeld(new string[] { "l1" }))
        {
            //block
            player.TransitionToState(player.stateBlocking);
        }

        if (SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" }))
        {
            //run
            player.TransitionToState(player.stateRunning);
        }


    }
}

 
 */