using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaggerState : PlayerBaseState
{
    float time;
    float speedOfClip = 1;
    float timeOffset = 0.25f;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("stagger", 0f);
        player.setInfoText("State: Stagger");

        time = 0;
    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        time += Time.deltaTime;

        if (time >= (player.clips["stagger"].length - timeOffset) / speedOfClip)
        {
            player.snapToGround = true;

            player.TransitionToState(player.stateIdle);
        }
    }

}
