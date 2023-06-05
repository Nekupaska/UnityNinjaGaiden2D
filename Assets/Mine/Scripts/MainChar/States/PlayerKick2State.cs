using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick2State : PlayerBaseState
{
    float time;
    float speedOfClip = 2f;
    float movableTimeOffset = 0.25f;
    bool squarePressed = false;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("kick2", 0.15f);
        player.setInfoText("State: Kick 2");

        time = 0;

        squarePressed = false;


        player.launchAttack(player.attackColliders[0], true, 20);

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }



    public override void Update(MainChar player)
    {

        time += Time.deltaTime;

        if (SimpInput.getIfPressed(new string[] { "s" }))
        {
            //punch2
            //player.TransitionToState(player.stateJumping);
        }

        if (SimpInput.getIfPressed(new string[] { "c" }))
        {
            //throw shuriken
            player.TransitionToState(player.stateShurikenGround);
        }

        if (time >= player.clips["kick2"].length * speedCalc())
        {
            player.TransitionToState(player.stateIdle);
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

public class PlayerBlockState : PlayerBaseState
{
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("block", 0.15f);
        player.setInfoText("State: Blocking");

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {

        if (SimpInput.getIfReleased(new string[] { "l1" }))
        {
            //idle
            player.TransitionToState(player.stateIdle);
        }

        if (SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" }))
        {
            //roll
            player.updateRotation();
            player.TransitionToState(player.stateRolling);
        }

    }

}

 */