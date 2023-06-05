using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGroundEnemyOld : MonoBehaviour
{
    public Transform playerTransform; //Spine2
    public GameObject player;

    Vector3 originalPosition;

    Animator anim;
    public CharacterController con;

    bool closeToPlayerButNotTooClose = false;
    bool closeEnough = false;

    float minRandomRange = 90;

    float detectionDistance = 5f;
    float attackingDistance = 0.8f;

    float positionToPlayer;

    float speedMult = 0.07f;
    float normalSpeed = 0.5f;
    float chaseSpeed = 1;
    float attackSpeed = 2;
    float gravitySpeed = 0.2f;

    Vector3 velocity = Vector3.zero;


    public enum State { idle, attacking, following, stagger };
    public State state;
    float time;
    public bool isColliding = false;

    //audio
    public AudioClip audHit;


    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        originalPosition = this.transform.position;

        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("dummy").transform;
        }

        if (player == null)
        {
            player = GameObject.Find("dummy");
        }

    }

    void FixedUpdate()
    {
        positionToPlayer = Vector3.Distance(this.transform.position, playerTransform.position);
        closeToPlayerButNotTooClose = positionToPlayer <= detectionDistance;
        closeEnough = positionToPlayer <= attackingDistance;

        updateState();

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
                anim.Play("idle");
                break;
            case State.attacking:
                anim.Play("attack");
                break;
            case State.following:
                break;
            case State.stagger:
                anim.Play("hit");
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
                if (closeToPlayerButNotTooClose)
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
                if (closeEnough)
                {
                    if (isInRandomRange())
                    {
                        transitionState(State.attacking);
                    }

                }

                gravity();

                //move(velocity);

                break;

            case State.stagger:
                if (time > 0.7f)
                {
                    transitionState(State.idle);
                }
                break;
        }

        move(velocity);
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

    public bool isGrounded()
    {
        return con.isGrounded;
    }

    private Vector3 getLowerCenterOfCharacterController()
    {
        return new Vector3(transform.position.x, transform.position.y + con.radius, transform.position.z);
    }

    private void getCloser()
    {
        float right = this.transform.position.z - playerTransform.position.z;//if negative, player is on its right
        float sign = Mathf.Sign(right);

        //if (sign < 0)
        //{
        //    velocity.z = chaseSpeed;
        //}
        //else if (sign > 0)
        //{
        //    velocity.z = -chaseSpeed;
        //}

        velocity.z = chaseSpeed * -sign * Time.deltaTime;

        

    }

    private void gravity()
    {
        if (!isGrounded())
        {
            velocity.y -= gravitySpeed * Time.deltaTime;

        }
        else
        {
            velocity = new Vector3(velocity.x, 0, velocity.z);
        }
    }
    public void move(Vector3 v)
    {
        con.Move(v);
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
