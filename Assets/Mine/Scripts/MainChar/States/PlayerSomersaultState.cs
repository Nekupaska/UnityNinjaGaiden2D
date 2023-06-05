using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSomersaultState : PlayerBaseState
{
    float time;
    float speedOfClip = 2;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("somersault", 0.15f);
        player.setInfoText("State: Somersault");
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

        v = player.fallVector(); //player.fall();
        v.z = player.moveFowardVector(player.moveSpeed * 1.8f).z; ; //player.moveFoward(player.moveSpeed * 1.8f);

        player.move(v);

        //player.updateRotation();

        //wallrun
        if (player.isFacingWall())
        {
            player.TransitionToState(player.stateWallrunning); //
        }

        if (SimpInput.getIfHeld(new string[] { "t" }))
        {
            //flying swallow
            player.TransitionToState(player.stateFlyingSwallow);
        }

        if (SimpInput.getIfPressed(new string[] { "c" }) && player.canThrowAirShuriken)
        {
            player.TransitionToState(player.stateShurikenAir);
        }

        //if (player.isAnimFree("somersault"))
        if (time >= player.clips["somersault"].length / speedOfClip)
        {
            player.snapToGround = true;
            player.TransitionToState(player.stateIdle);
        }
    }

}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSomersaultState : PlayerBaseState
{
    float time;
    float speedOfClip = 2;

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("somersault", 0.15f);
        player.setInfoText("State: Somersault");
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
        //player.updateRotation();
        if (SimpInput.getIfHeld(new string[] { "t" }))
        {
            //flying swallow
            player.TransitionToState(player.stateFlyingSwallow);
        }

        //if (player.isAnimFree("somersault"))
        if (time >= player.clips["somersault"].length / speedOfClip)
        {
            player.TransitionToState(player.stateIdle);
        }
    }

}

 */