using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Animator anim;
    public CharacterController con;
    public Rigidbody rb;

    public Vector3 velocity;
    public float speed = 10;
    //public List<Action> actions;

    float normalCenter = 0.87f;
    float normalHeight = 1.77f;

    float jumpCenter = 1.79f;
    float jumpHeight = 1.77f;


    Action currentAction;

    //actions
    //Action walkLeft = new Action("walk", new string[] { "l" });
    //Action walkRight = new Action("walk", new string[] { "r" });

    Action runLeft = new Action("run", new string[] { "l" });
    Action runRight = new Action("run", new string[] { "r" });

    Action somersault = new Action("somersault", new string[] { "x" });
    Action jump = new Action("jump", new string[] { "x" });

    Action roll = new Action("walk");

    Action idle = new Action("idle");



    //states
    public enum PhysicalStates { grounded, airborne, stagger }
    public PhysicalStates physicalState;

    public enum ActionStates { none, idle, running, jumping, somersault, wallrunning }
    public ActionStates actionState;
    public bool blocking = false;

    public bool right = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        con = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        //rb.useGravity = false;


        //actions = new List<Action>();

        somersault.startOffset = 0.15f;
        somersault.cancellable = true;
        jump.cancellable = true;



    }


    void Update()
    {
        debug();

        if (!GLOBAL_VARIABLES.isPaused)
        {
            anim.speed = 1;

            //SOMERSAULT&JUMP
            if ((SimpInput.getIfPressed(somersault.input)))
            {
                //if ((SimpInput.getIfPressed(somersault.input) && (SimpInput.getIfHeld(runLeft.input) || SimpInput.getIfHeld(runRight.input))) && somersault.isFree(anim))
                if ((SimpInput.getIfHeld(runLeft.input) || SimpInput.getIfHeld(runRight.input)))
                {
                    //state: somersaulting
                    if (somersault.isFree(anim))
                    {
                        actionState = ActionStates.somersault;
                        physicalState = PhysicalStates.airborne;
                    }
                    else
                    {
                        actionState = ActionStates.none;
                        physicalState = PhysicalStates.grounded;
                    }
                }
                else
                {
                    actionState = ActionStates.jumping;
                    physicalState = PhysicalStates.airborne;
                }
            }

            //MOVING
            if (actionState != ActionStates.somersault || physicalState != PhysicalStates.airborne)
                if (somersault.isFree(anim))
                {

                    if (SimpInput.getIfHeld(runLeft.input) || SimpInput.getIfHeld(runRight.input))
                    {
                        //state: running
                        actionState = ActionStates.running;

                        if (SimpInput.getIfHeld(runLeft.input))
                        {
                            right = false;
                        }

                        if (SimpInput.getIfHeld(runRight.input))
                        {
                            right = true;
                        }

                    }
                    else
                    {
                        //state: idle
                        actionState = ActionStates.idle;
                    }

                }
        }
        else
        {
            anim.speed = 0; //pause everything
        }

        doAction();
        doPhysics();

    }

    void doAction()
    {
        switch (actionState)
        {
            case ActionStates.idle:
                idle.crossFadeLoop(anim);
                break;

            //case ActionStates.walking: break;

            case ActionStates.running:
                runLeft.playLoop(anim);
                moveFoward(speed);


                break;

            case ActionStates.jumping:
                if (jump.isFree(anim))
                {
                    jump.play(anim, 0, -1);
                }
                break;

            case ActionStates.somersault:
                if (somersault.isFree(anim))
                {
                    somersault.play(anim, 0, -1);
                }
                moveFoward(speed);

                break;
        }

        //setRotation();
    }

    void moveFoward(float speed)
    {
        if (right)
        {
            con.SimpleMove(Vector3.forward * speed);
        }
        else
        {
            con.SimpleMove(Vector3.back * speed);
        }
    }

    void doPhysics()
    {
        print(actionState);
        print(physicalState);
        switch (physicalState)
        {
            case PhysicalStates.grounded:
                break;
            case PhysicalStates.airborne:
                break;
            case PhysicalStates.stagger: break;
        }
        setRotation();



    }

    void debug()
    {
        velocity = rb.velocity;
    }

    public void setRotation()
    {
        if (right)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Animator anim;
    public CharacterController con;
    public Rigidbody rb;

    public Vector3 velocity;
    public float speed = 10;
    //public List<Action> actions;

    float normalCenter = 0.87f;
    float normalHeight = 1.77f;

    float jumpCenter = 1.79f;
    float jumpHeight = 1.77f;


    Action currentAction;

    //actions
    //Action walkLeft = new Action("walk", new string[] { "l" });
    //Action walkRight = new Action("walk", new string[] { "r" });

    Action runLeft = new Action("run", new string[] { "l" });
    Action runRight = new Action("run", new string[] { "r" });

    Action somersault = new Action("somersault", new string[] { "x" });

    Action roll = new Action("walk");

    Action idle = new Action("idle");



    //states
    public enum PhysicalStates { grounded, airborne, stagger }
    public PhysicalStates physicalState;

    public enum ActionStates { none, idle, running, jumping, somersault, wallrunning }
    public ActionStates actionState;
    public bool blocking = false;

    public bool right = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        con = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        //rb.useGravity = false;


        //actions = new List<Action>();

        somersault.startOffset = 0.15f;
        somersault.cancellable = true;




    }


    void Update()
    {
        debug();

        if (!GLOBAL_VARIABLES.isPaused)
        {
            anim.speed = 1;

            //SOMERSAULT
            if ((SimpInput.getIfPressed(somersault.input) && (SimpInput.getIfHeld(runLeft.input) || SimpInput.getIfHeld(runRight.input))) && somersault.isFree(anim))
            {
                //state: somersaulting
                actionState = ActionStates.somersault;
                physicalState = PhysicalStates.airborne;
            }
            else
            {
                if (somersault.isFree(anim))
                {
                    actionState = ActionStates.none;
                    physicalState = PhysicalStates.grounded;
                }
            }

            //MOVING
            if (actionState != ActionStates.somersault || physicalState != PhysicalStates.airborne)
                if (somersault.isFree(anim))
                {

                    if (SimpInput.getIfHeld(runLeft.input) || SimpInput.getIfHeld(runRight.input))
                    {
                        //state: running
                        actionState = ActionStates.running;

                        if (SimpInput.getIfHeld(runLeft.input))
                        {
                            right = false;
                        }

                        if (SimpInput.getIfHeld(runRight.input))
                        {
                            right = true;
                        }

                    }
                    else
                    {
                        //state: idle
                        actionState = ActionStates.idle;
                    }

                }
        }
        else
        {
            anim.speed = 0; //pause everything
        }

        doAnimation();
        doPhysics();

    }

    void doAnimation()
    {
        switch (actionState)
        {
            case ActionStates.idle:
                idle.crossFadeLoop(anim);
                break;

            //case ActionStates.walking: break;

            case ActionStates.running:
                runLeft.playLoop(anim);
                move(speed);


                break;

            case ActionStates.jumping: break;

            case ActionStates.somersault:
                if (somersault.isFree(anim))
                {
                    somersault.play(anim, 0, -1);
                }
                move(speed);

                break;
        }

        //setRotation();
    }

    void move(float speed)
    {
        if (right)
        {
            con.SimpleMove(Vector3.forward * speed);
        }
        else
        {
            con.SimpleMove(Vector3.back * speed);
        }
    }

    void doPhysics()
    {
        print(actionState);
        print(physicalState);
        switch (physicalState)
        {
            case PhysicalStates.grounded:
                break;
            case PhysicalStates.airborne:
                break;
            case PhysicalStates.stagger: break;
        }
        setRotation();



    }

    void debug()
    {
        velocity = rb.velocity;
    }

    public void setRotation()
    {
        if (right)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}

 
 
 */