using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Act
{
    public string animName;
    public List<Act> allowedActs = new List<Act>();
    public bool cancellable=false;
    public float startOffset;
    public string[] input;
    public float moveSpeed = 0;

    public override string ToString()
    {
        return this.animName;
    }

    public Act(string animName)
    {
        this.animName = animName;
    }

    public Act(string animName, string[] input)
    {
        this.animName = animName;
        this.input = input;

    }

    public Act(string animName, bool cancellable)
    {
        this.animName = animName;
        this.cancellable = cancellable;
    }

    public Act(string animName, List<Act> allowedActs)
    {
        this.animName = animName;
        this.allowedActs = allowedActs;
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

    public void setTrigger(Animator anim)
    {
        anim.SetTrigger(animName);
    }

    public bool isFree(Animator anim)
    {
        //Here I must buffer the inputs. I'll try to check this method until it returns true (it's free). That means the next action can be done, which also means I need to iterate the allowed move list, check the buffer, see if any match, else return null from getInputAct()


        //return !anim.GetCurrentAnimatorStateInfo(0).IsName(animName) || cancellable;
        return !anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    public Act getInputAct()
    {
        Act result = null;
        //result = MainChar.idle;

        var input = SimpInput.getLastBufferedInput();
        //var input = SimpInput.getLastInput();

        if (input != null && input.Length > 0)
            foreach (Act a in allowedActs)
            {
                //if (a.input == input)
                if (compareStringArray(a.input, input))
                {
                    result = a;
                }
            }

        return result;
    }

    private bool compareStringArray(string[] one, string[] two)
    {
        bool equal = true;

        //first we check if the length is the same
        if (one.Length == two.Length)
        {
            //then we iterate
            for (int i = 0; i < one.Length; i++)
            {
                //if any of their elements doesn't match, they're not equal
                if (one[i] != two[i])
                {
                    equal = false;
                }
            }
        }

        return equal;


    }
}