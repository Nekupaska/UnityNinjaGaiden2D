using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBirdflipState : PlayerBaseState
{
    float time;
    float speedOfClip = 1f;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("birdflip", 0);
        player.setInfoText("State: Birdflip");

        time = 0;

        player.velocity.y = player.jumpSpeed * 2;
        player.flip();
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
        v = player.fallVector(); //player.fall();
        v.z = player.moveFowardVector(-player.jumpSteerSpeed * 2).z;



        if (player.isWallBehind() && SimpInput.getIfPressed(new string[] { "x" }))
        {
            //birdflip

            player.TransitionToState(player.stateBirdflip);
        }

        if (SimpInput.getIfPressed(new string[] { "c" }) && player.canThrowAirShuriken)
        {
            player.flip();
            player.TransitionToState(player.stateShurikenAir);
        }

        player.move(v);

        if (time >= player.clips["birdflip"].length / speedOfClip)
        {
            //player.velocity.y = player.jumpSpeed;
            //player.flip();
            player.snapToGround = true;
            player.TransitionToState(player.stateFalling);
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