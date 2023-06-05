using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    float time = 0;
    float speedOfClip = 1;
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("jump", 0.15f);
        player.setInfoText("State: Jumping");
        time = 0;
        player.velocity = Vector3.up * player.jumpSpeed;
        //player.shrink();
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

        if (SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" }))
        {
            player.updateRotation();
            v.z = player.moveFowardVector(player.jumpSteerSpeed).z; //player.moveFoward(player.jumpSteerSpeed);
        }

        if (SimpInput.getIfPressed(new string[] { "c" }) && player.canThrowAirShuriken)
        {
            player.TransitionToState(player.stateShurikenAir);
        }


        player.con.Move(v);


        if (player.isGrounded() && (time >= player.clips["jump"].length / speedOfClip))
        {
            //player.unshrink();
            player.snapToGround = true;

            player.TransitionToState(player.stateIdle);
        }

    }


}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    float time = 0;
    float speedOfClip = 1;
    public override void EnterState(MainChar player)
    {
        player.anim.CrossFade("jump", 0.15f);
        player.setInfoText("State: Jumping");
        time = 0;
        player.velocity = Vector3.up * player.jumpSpeed;
        player.shrink();
    }

    public override void OnCollisionEnter(MainChar player)
    {

    }


    Vector3 v;
    public override void Update(MainChar player)
    {
        time += Time.deltaTime;

        v = player.fallVector(); //player.fall();

        if (SimpInput.getIfHeld(new string[] { "l" }) || SimpInput.getIfHeld(new string[] { "r" }))
        {
            player.updateRotation();
            v.z = player.moveFowardVector(player.jumpSteerSpeed).z; //player.moveFoward(player.jumpSteerSpeed);
        }

        player.con.Move(v);


        if (player.isGrounded() && (time >= player.clips["jump"].length / speedOfClip))
        {
            //player.unshrink();
            player.TransitionToState(player.stateIdle);
        }

    }


}


 */