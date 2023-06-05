using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplatState : PlayerBaseState
{
    float time;
    float speedOfClip = 1;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("fallSplat", 0.15f);
        player.setInfoText("State: Splat");

        time = 0;
    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        time += Time.deltaTime;

        if (time >= player.clips["fallsplat"].length / speedOfClip)
        {
            player.snapToGround = true;

            player.TransitionToState(player.stateIdle);
        }
    }

}
