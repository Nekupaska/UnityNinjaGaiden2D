using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlyingEnemy : MonoBehaviour
{
    public Transform playerPosition; //Spine2
    public GameObject player;

    Vector3 originalPosition;

    Rigidbody rb;
    Animator anim;

    bool closeToPlayerButNotTooClose = false;
    bool closeEnough = false;

    bool ableToAttack = true;
    float minRandomRange = 90;

    float detectionDistance = 5f;
    float attackingDistance = 0.8f;

    float positionToPlayer;

    float speedMult = 0.07f;
    float normalSpeed = 0.5f;
    float chaseSpeed = 1;
    float attackSpeed = 2;

    //bool attacking = false;

    Vector3 trajectory;


    public enum State { idle, attacking, following, returning, stagger };
    public State state;
    float time;
    public bool isColliding = false;

    //audio
    public AudioClip audHit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        originalPosition = this.transform.position;

        if (playerPosition == null)
        {
            playerPosition = GameObject.Find("dummy").transform;
        }

        if (player == null)
        {
            player = GameObject.Find("dummy");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (SimpInput.getIfPressed(new string[] { "se" })) //select
        //{
        //    GameObject.Find("dummy").GetComponent<MainChar>().getHit();
        //}

        positionToPlayer = Vector3.Distance(this.transform.position, playerPosition.position);
        closeToPlayerButNotTooClose = positionToPlayer <= detectionDistance;
        closeEnough = positionToPlayer <= attackingDistance;

        updateState(); //update of all states

    }

    public void transitionState(State state)
    {
        //print(state);
        this.state = state;
        enterState();
    }

    void enterState()
    {
        time = 0;

        switch (state)
        {
            case State.idle:
                break;
            case State.attacking:
                anim.Play("attack");
                break;
            case State.following:
                break;
            case State.returning:
                break;
            case State.stagger:
                anim.Play("idle");
                //print("flying enemy hit");
                break;
        }
    }

    void updateState()
    {
        time += Time.deltaTime;

        switch (state)
        {
            case State.idle:
                if (closeToPlayerButNotTooClose) //won't move until player is close to it. If not in original position, it will return to it
                {
                    transitionState(State.following);
                }

                break;

            case State.attacking:
                if (isColliding && time >= 0.1f && time < 0.15f)//peak of attack
                {
                    isColliding = false;
                    player.GetComponent<MainChar>().getHit();
                    //print("hitting");
                }

                if (time > 0.5f)
                {
                    transitionState(State.idle);
                }
                break;

            case State.following:
                getCloser();
                if (closeEnough) //if it's close enough to the player, attack or stay put. Else pursue
                {
                    if (isInRandomRange()) //will attack if able to attack and random chance is true
                    {
                        transitionState(State.attacking);
                    }

                }
                else
                {
                    if (!closeToPlayerButNotTooClose) //won't move until player is close to it. If not in original position, it will return to it
                    {
                        transitionState(State.returning);
                    }
                }


                break;

            case State.returning:
                rb.MovePosition(Vector3.MoveTowards(this.transform.position, originalPosition, speedMult * normalSpeed));

                if (closeToPlayerButNotTooClose) //won't move until player is close to it. If not in original position, it will return to it
                {
                    transitionState(State.following);
                }

                if (transform.position == originalPosition)
                {
                    transitionState(State.idle);
                }
                break;
            case State.stagger:
                if (time > 0.7f)
                {
                    transitionState(State.idle);
                }
                break;
        }
    }

    public void getHit(bool launches)
    {
        playClip(audHit);

        if (launches)
        {

        }
        //print(time);
        transitionState(State.stagger);
    }

    /*void OnTriggerEnter(Collider col)
    {


        var obj = col.gameObject;
        if (obj.CompareTag("player"))
        {
            player = obj;
            isColliding = true;
            //obj.GetComponent<MainChar>().getHit();
        }
    }

    void OnTriggerExit(Collider col)
    {
        var obj = col.gameObject;

        if (obj.CompareTag("player"))
        {
            player = null;
            isColliding = false;
        }
    }*/

    private void getCloser()
    {
        rb.MovePosition(Vector3.MoveTowards(this.transform.position, playerPosition.position, speedMult * chaseSpeed));
    }

    private void goThrough()
    {
        //rb.MovePosition(Vector3.MoveTowards(this.transform.position, player.position, speedMult * attackSpeed));
        rb.MovePosition(trajectory * speedMult);
    }

    private bool isInRandomRange()
    {
        float randNum;

        randNum = Random.Range(0, 100);
        bool inRange = minRandomRange >= randNum;

        return inRange;
    }

    public void playClip(AudioClip clip)
    {
        if (clip != null)
            GetComponent<AudioSource>().PlayOneShot(clip);
    }

}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlyingEnemy : MonoBehaviour
{
    public Transform player;

    Vector3 originalPosition;

    Rigidbody rb;
    Animator anim;

    bool closeToPlayerButNotTooClose = false;
    bool closeEnough = false;

    bool ableToAttack = true;
    float minRandomRange = 90;

    float detectionDistance = 5f;
    float attackingDistance = 0.8f;

    float positionToPlayer;

    float speedMult = 0.07f;
    float normalSpeed = 0.5f;
    float chaseSpeed = 1;
    float attackSpeed = 2;

    //bool attacking = false;

    Vector3 trajectory;


    enum State { idle, attacking, following, returning};
    State state;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        originalPosition = this.transform.position;

        if (player == null)
        {
            player = GameObject.Find("dummy").transform;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        positionToPlayer = Vector3.Distance(this.transform.position, player.position);
        closeToPlayerButNotTooClose = positionToPlayer <= detectionDistance;
        closeEnough = positionToPlayer <= attackingDistance;

        enterState();
        updateState();

        if (closeToPlayerButNotTooClose) //won't move until player is close to it. If not in original position, it will return to it
        {
            if (closeEnough) //if it's close enough to the player, attack or stay put. Else pursue
            {
                if (ableToAttack && isInRandomRange()) //will attack if able to attack and random chance is true
                {
                    //attack();
                    StartCoroutine("enableAttack", 2.5f); //waits for x seconds then sets ableToAttack to true
                }
            }
            else
            {
                getCloser();
            }
        }
        else
        {
            rb.MovePosition(Vector3.MoveTowards(this.transform.position, originalPosition, speedMult * normalSpeed));
        }


        //This is just so that it keeps moving while attacking
        if (!ableToAttack)
        {
            attack();
        }
    }

    void TransitionState()
    {
        
    }

    void enterState()
    {
        switch (state)
        {
            case State.idle:
                break;
            case State.attacking:
                break;
            case State.following:
                break;
            case State.returning:
                break;
        }
    }

    void updateState()
    {
        switch (state)
        {
            case State.idle:
                break;
            case State.attacking:
                break;
            case State.following:
                break;
            case State.returning:
                break;
        }
    }

    

    private void attack()
    {
        //goThrough();
        anim.Play("attack");

        //print("I'm attacking!");
    }

    private void getCloser()
    {
        rb.MovePosition(Vector3.MoveTowards(this.transform.position, player.position, speedMult * chaseSpeed));
    }

    private void goThrough()
    {
        //rb.MovePosition(Vector3.MoveTowards(this.transform.position, player.position, speedMult * attackSpeed));
        rb.MovePosition(trajectory * speedMult);
    }

    private bool isInRandomRange()
    {
        float randNum;

        randNum = Random.Range(0, 100);
        bool inRange = minRandomRange >= randNum;

        return inRange;
    }

    private IEnumerator enableAttack(float waitTime)
    {
        trajectory = Vector3.MoveTowards(this.transform.position, player.position, speedMult * attackSpeed);
        ableToAttack = false;
        yield return new WaitForSeconds(waitTime);
        ableToAttack = true;

    }
}

 
 
 */