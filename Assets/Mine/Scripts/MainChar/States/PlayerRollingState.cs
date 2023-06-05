using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    bool somersaultWasPressed = false;
    bool cancellable = true;
    float time;
    float speedOfClip = 3;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("roll", 0.15f);
        player.setInfoText("State: Rolling");
        somersaultWasPressed = false;
        time = 0;
        player.invulnerable = true;

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        player.fall();

        time += Time.deltaTime;

        player.moveFoward(player.moveSpeed * 1.8f);

        if (SimpInput.getIfHeld(new string[] { "x" }))
        {
            //somersault
            //somersaultWasPressed = true;
            player.invulnerable = false;

            player.updateRotation();
            player.TransitionToState(player.stateSomersault);
        }

        if (time >= player.clips["roll"].length / speedOfClip)
        {
            player.invulnerable = false;

            player.TransitionToState(player.stateIdle);
        }
    }

}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    bool somersaultWasPressed = false;
    bool cancellable = true;
    float time;
    float speedOfClip = 3;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("roll", 0.15f);
        player.setInfoText("State: Rolling");
        somersaultWasPressed = false;
        time = 0;

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        player.fall();

        time += Time.deltaTime;

        player.moveFoward(player.moveSpeed * 1.8f);

        if (SimpInput.getIfHeld(new string[] { "x" }))
        {
            //somersault
            //somersaultWasPressed = true;
            player.updateRotation();
            player.TransitionToState(player.stateSomersault);
        }

        if (time >= player.clips["roll"].length / speedOfClip)
        {
            player.TransitionToState(player.stateIdle);
        }
    }

}

 */