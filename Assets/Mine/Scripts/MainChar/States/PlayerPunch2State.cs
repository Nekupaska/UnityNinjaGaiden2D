using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch2State : PlayerBaseState
{
    float time;
    float speedOfClip = 1f;
    float movableTimeOffset = 0.75f;
    bool squarePressed = false;
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("punch2", 0.15f);
        player.setInfoText("State: Punch 2");

        time = 0;
        squarePressed = false;

        player.launchAttack(player.attackColliders[0], false,10);

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }



    public override void Update(MainChar player)
    {

        time += Time.deltaTime;

        if (SimpInput.getIfPressed(new string[] { "s" }))
        {

            squarePressed = true;
        }

        if (SimpInput.getIfPressed(new string[] { "c" }))
        {
            //throw shuriken
            player.TransitionToState(player.stateShurikenGround);
        }

        if (squarePressed && time >= (player.clips["punch1"].length * speedCalc()) - movableTimeOffset)
        {
            //kick2
            player.TransitionToState(player.stateKick2);

        }

        if (time >= player.clips["punch2"].length * speedCalc())
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