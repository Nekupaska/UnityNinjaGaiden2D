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