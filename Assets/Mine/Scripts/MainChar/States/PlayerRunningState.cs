using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    float time;
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("run", 0.15f);
        player.setInfoText("State: Running");
        time = 0;

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    Vector3 v;


    public override void Update(MainChar player)
    {
        //run
        //player.fall();

        //player.moveFoward(player.moveSpeed);
        time += Time.deltaTime;
        v = player.fallVector();
        v.z = player.moveFowardVector(player.moveSpeed).z;


        player.con.Move(v);
        player.updateRotation();

        if (player.isGrounded())
        {

            if (SimpInput.getIfPressed(new string[] { "s" }))
            {
                player.TransitionToState(player.statePunch1);
            }

            if (SimpInput.getIfPressed(new string[] { "x" }))
            {
                //somersault
                player.TransitionToState(player.stateSomersault);
            }

            if (SimpInput.getIfPressed(new string[] { "l1" }))
            {
                //roll
                player.TransitionToState(player.stateRolling);
            }

            if (SimpInput.getIfPressed(new string[] { "c" }))
            {
                //throw shuriken
                player.TransitionToState(player.stateShurikenGround);
            }
        }

        if (!(SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" })))
        {
            //idle
            player.TransitionToState(player.stateIdle);
        }

    }

}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{

    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("run", 0.15f);
        player.setInfoText("State: Running");

    }

    public override void OnCollisionEnter(MainChar player)
    {

    }

    public override void Update(MainChar player)
    {
        //run
        player.fall();

        player.moveFoward(player.moveSpeed);
        player.updateRotation();


        if (SimpInput.getIfPressed(new string[] { "x" }))
        {
            //somersault
            player.TransitionToState(player.stateSomersault);
        }

        if (SimpInput.getIfPressed(new string[] { "l1" }))
        {
            //roll
            player.TransitionToState(player.stateRolling);
        }

        if (!(SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" })))
        {
            //idle
            player.TransitionToState(player.stateIdle);
        }

    }

}

 */