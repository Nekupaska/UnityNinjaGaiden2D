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

        if (player.isGrounded())
        {

            if (SimpInput.getIfPressed(new string[] { "s" }))
            {
                player.TransitionToState(player.statePunch1);
            }

            if (SimpInput.getIfPressed(new string[] { "x" }))
            {
                //normal jump
                player.TransitionToState(player.stateJumping);
            }

            if (SimpInput.getIfPressed(new string[] { "x","s" }))
            {
                //normal jump
                player.TransitionToState(player.stateSomersault);
            }

            if (SimpInput.getIfHeld(new string[] { "l1" }))
            {
                //block
                player.TransitionToState(player.stateBlocking);
            }

            if (SimpInput.getIfPressed(new string[] { "c" }))
            {
                //throw shuriken
                player.TransitionToState(player.stateShurikenGround);
            }

        }
        else
        {
            //maybe I should separate this into another state, falling state, then encapsulate it. Then from the falling state, if grounded, go back to idle

            //if (SimpInput.getIfPressed(new string[] { "c" }) && player.canThrowAirShuriken)
            //{
                player.TransitionToState(player.stateFalling);
            //}
        }

        if (SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" }))
        {
            //run
            player.TransitionToState(player.stateRunning);
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