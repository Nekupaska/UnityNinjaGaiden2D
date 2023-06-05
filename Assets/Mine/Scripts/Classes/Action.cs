using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Action
{
    public string animName;
    public List<Action> allowedActions;
    public bool cancellable;
    public float startOffset;
    public string[] input;

    public override string ToString()
    {
        return this.animName;
    }

    public Action(string animName)
    {
        this.animName = animName;
    }

    public Action(string animName, string[] input)
    {
        this.animName = animName;
        this.input = input;
    }

    public Action(string animName, bool cancellable)
    {
        this.animName = animName;
        this.cancellable = cancellable;
    }

    public Action(string animName, List<Action> allowedActions)
    {
        this.animName = animName;
        this.allowedActions = allowedActions;
    }

    public void playLoop(Animator anim)
    {
        anim.Play(animName);
    }

    public void crossFadeLoop(Animator anim)
    {


        anim.CrossFade(animName, 0.05f, (int)PlayMode.StopSameLayer, 0);

        //Was pretty self explanatory...

        //anim.loop = true;


    }

    public void play(Animator anim, float offset)
    {
        if (offset == -1)
        {
            offset = this.startOffset;
        }

        if (isFree(anim) || cancellable)
        {
            anim.CrossFade(animName, 0.5f, 0, offset);
        }
    }

    public void play(Animator anim, float crossfadeAmount, float offset)
    {
        if (offset == -1)
        {
            offset = this.startOffset;
        }

        if (isFree(anim) || cancellable)
        {
            anim.CrossFade(animName, crossfadeAmount, 0, offset);
        }
    }

    public bool isFree(Animator anim)
    {
        return !anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    public Action getInputAction(string input)
    {
        Action result = null;
        foreach (Action a in allowedActions)
        {
            if (a.animName == input)
            {
                result = a;
            }
        }

        return result;
    }

}
