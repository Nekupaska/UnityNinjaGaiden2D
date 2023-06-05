using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGroundEnemy : MonoBehaviour
{
    public Transform playerTransform; //Spine2
    public GameObject player;

    Vector3 originalPosition;

    public Animator anim;
    public Rigidbody rb;
    public CapsuleCollider cc;

    bool closeToPlayerButNotTooClose = false;
    bool closeEnough = false;

    float minRandomRange = 90;

    float detectionDistance = 5f;
    float attackingDistance = 1.2f;

    float positionToPlayer;

    float speedMult = 0.07f;
    float normalSpeed = 0.5f;
    float chaseSpeed = 2;
    float attackSpeed = 2;
    float gravitySpeed = 0.3f;

    public Vector3 velocity = Vector3.zero;


    public enum State { idle, attacking, following, stagger };
    public State state;
    float time;
    public bool isColliding = false;

    //audio
    public AudioClip audHit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        if (anim == null) anim = GetComponent<Animator>();
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
                anim.CrossFade("attack", 0.2f);
                break;
            case State.following:
                break;
            case State.stagger:
                anim.CrossFade("hit", 0.2f);
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
                //velocity = Vector3.Lerp(velocity, Vector3.zero, 0.6f);
                velocity = Vector3.zero;
                gravity();
                if (closeToPlayerButNotTooClose)
                {
                    transitionState(State.following);
                }

                break;

            case State.attacking:
                if (isColliding && time >= 0.1f && time < 0.3f)//peak of attack
                {
                    isColliding = false;
                    player.GetComponent<MainChar>().getHit();
                    //print("hitting");
                }

                if (time > 2f)
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
                if (time > 0.1f)
                {
                    gravity();//this is because if it's close to the ground, the vector.y will be set to 0 in gravity() and thus not launched. 
                    getAway();
                }

                if (time > 0.7f && isGrounded())
                {
                    transitionState(State.idle);
                }
                break;
        }

        move(velocity);
    }

    public void getHit(bool launches, float amount)
    {
        playClip(audHit);
        velocity = Vector3.zero;
        if (launches || !isGrounded())
        {
            velocity = Vector3.up * amount * 0.008f;
        }
        //print(time);
        transitionState(State.stagger);
    }

    public bool isGrounded()
    {
        float groundOffset = 0.05f;
        float dist = 0.15f;

        //float colliderBounds = GetComponent<CharacterController>().bounds.extents.y;
        float colliderBounds = GetComponent<CapsuleCollider>().bounds.extents.y;
        Vector3 pivot = new Vector3(transform.position.x, transform.position.y - (cc.height / 2), transform.position.z);
        Debug.DrawRay(pivot, Vector3.down * dist, Color.red, colliderBounds * dist * 2);

        RaycastHit r;

        bool gr = Physics.Raycast(pivot, Vector3.down * dist, out r, colliderBounds * dist * 2, LayerMask.GetMask("Level"));
        //bool gr = Physics.Raycast(transform.position, Vector3.down * 0.2f, out r, colliderBounds * 0.2f);
        //bool gr = Physics.SphereCast(getLowerCenterOfCharacterController(), cc.radius, Vector3.down * 0.2f, out r, colliderBounds * 0.2f, LayerMask.GetMask("Level"));

        return gr;
    }

    private Vector3 getLowerCenterOfCharacterController()
    {
        return new Vector3(transform.position.x, transform.position.y + cc.radius, transform.position.z);
    }

    private void getCloser()
    {
        float right = this.transform.position.z - playerTransform.position.z;//if negative, player is on its right
        float sign = Mathf.Sign(right);

        velocity.z = chaseSpeed * -sign * Time.deltaTime;



    }

    private void getAway()
    {
        float right = this.transform.position.z - playerTransform.position.z;
        float sign = Mathf.Sign(right);

        velocity.z = chaseSpeed / 3 * sign * Time.deltaTime;

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
        //rb.velocity = v * Time.deltaTime;
        rb.MovePosition(new Vector3(this.transform.position.x + v.x, this.transform.position.y + v.y, this.transform.position.z + v.z));
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
