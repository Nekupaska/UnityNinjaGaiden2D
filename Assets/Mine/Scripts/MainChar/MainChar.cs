using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MainChar : MonoBehaviour
{
    //General
    public Animator anim;
    public CharacterController con;
    public Rigidbody rb;

    //Minor states and variables
    public bool right = true;
    public float gravitySpeed = 0.2f;
    public float moveSpeed = 0.1f;
    public float jumpSpeed = 0.1f;
    public float jumpSteerSpeed = 0.01f;
    public bool invulnerable = false;

    private float initHeight;
    private float initCenterY;

    public Transform handPosition;
    public GameObject shuriken;

    public bool canThrowAirShuriken = true;
    public float trackRadius = 7f;

    //Physics
    public Vector3 velocity = new Vector3();
    public bool snapToGround = true;

    //AudioClips
    public AudioClip twoah;

    //AnimationClips, needed for timings
    public Dictionary<string, AnimationClip> clips =
    new Dictionary<string, AnimationClip>();

    //STATES
    private PlayerBaseState currentState;
    public readonly PlayerIdleState stateIdle = new PlayerIdleState();
    public readonly PlayerFallState stateFalling = new PlayerFallState();
    public readonly PlayerRunningState stateRunning = new PlayerRunningState();
    public readonly PlayerJumpingState stateJumping = new PlayerJumpingState();
    public readonly PlayerSomersaultState stateSomersault = new PlayerSomersaultState();
    public readonly PlayerRollingState stateRolling = new PlayerRollingState();
    public readonly PlayerBlockState stateBlocking = new PlayerBlockState();
    public readonly PlayerFlyingSwallowState stateFlyingSwallow = new PlayerFlyingSwallowState();
    public readonly PlayerWallrunningState stateWallrunning = new PlayerWallrunningState();
    public readonly PlayerBirdflipState stateBirdflip = new PlayerBirdflipState();
    public readonly PlayerShurikenGroundState stateShurikenGround = new PlayerShurikenGroundState();
    public readonly PlayerShurikenAirState stateShurikenAir = new PlayerShurikenAirState();

    //
    public readonly PlayerPunch1State statePunch1 = new PlayerPunch1State();
    public readonly PlayerPunch2State statePunch2 = new PlayerPunch2State();
    public readonly PlayerKick2State stateKick2 = new PlayerKick2State();

    //
    public readonly PlayerStaggerState stateStagger = new PlayerStaggerState();
    public readonly PlayerAirHitState stateAirHit = new PlayerAirHitState();
    public readonly PlayerSplatState stateSplatHit = new PlayerSplatState();

    //
    public Collider[] attackColliders;

    //Debug
    public Text info;
    public Text other;

    void Start()
    {
        anim = GetComponent<Animator>();
        con = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        //////////////populate clips
        foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
        {
            clips.Add(ac.name.ToLower(), ac);
        }
        //////////////


        initHeight = con.height;
        initCenterY = con.center.y;

        TransitionToState(stateIdle);

    }

    void FixedUpdate()
    {
        //this is just to lock the character to the 2D plane, not allowing Unity to push it on the depth axis aka being pushed aside
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    void Update()
    {
        currentState.Update(this);
        isFacingWall();


        string message = "Grounded: " + isGrounded();
        message += "\nSnappedToGround: " + snapToGround;
        message += "\nNextToWall: " + isFacingWall();
        message += "\nCanThrowAirShuriken: " + canThrowAirShuriken;
        setOtherText(message);

    }

    /*
    // void FixedUpdate()
    //{
    //    //this is just to lock the character to the 2D plane, not allowing Unity to push it on the depth axis aka being pushed aside
    //    transform.position = new Vector3(0, transform.position.y, transform.position.z);
    //}

    void FixedUpdate()
    {
        //this is just to lock the character to the 2D plane, not allowing Unity to push it on the depth axis aka being pu
        currentState.Update(this);
        
        isFacingWall();


        string message = "Grounded: " + isGrounded();
        message += "\nSnappedToGround: " + snapToGround;
        message += "\nNextToWall: " + isFacingWall();
        message += "\nCanThrowAirShuriken: " + canThrowAirShuriken;
        setOtherText(message);

    }
     */

    #region Basic Movement
    private Vector3 getDirectionVector()
    {
        if (right)
        {
            return Vector3.forward;
        }
        else
        {
            return Vector3.back;
        }
    }

    public void moveFoward(float speed)
    {
        con.Move(moveFowardVector(speed));
    }

    public Vector3 moveFowardVector(float speed)
    {
        return getDirectionVector() * speed;
    }

    public void fall()
    {
        if (!isGrounded())
        {
            velocity.y -= gravitySpeed * Time.deltaTime;

            con.Move(velocity);
        }
        else
        {
            velocity = new Vector3(velocity.x, 0, velocity.z);
        }
    }

    public Vector3 fallVector()
    {
        Vector3 v = velocity;
        if (!isGrounded())
        {
            velocity.y -= gravitySpeed * Time.deltaTime;

            v = velocity;
        }

        return v;
    }


    public void move(Vector3 v)
    {
        con.Move(v);
    }

    public bool isGrounded()
    {
        float groundOffset = 0.05f;

        float colliderBounds = GetComponent<CharacterController>().bounds.extents.y;
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red, colliderBounds * 0.2f);

        RaycastHit r;

        //bool gr = Physics.Raycast(transform.position, Vector3.down * 0.2f, out r, colliderBounds * 0.2f);
        bool gr = Physics.SphereCast(getLowerCenterOfCharacterController(), con.radius, Vector3.down * 0.2f, out r, colliderBounds * 0.2f, LayerMask.GetMask("Level"));


        if (gr)
        {
            canThrowAirShuriken = true;
            if (snapToGround)
            {

                //prrint(Vector3.Angle(r.normal,Vector3.down)+"");
                transform.position = new Vector3(transform.position.x, r.point.y, transform.position.z);
            }
        }

        return gr;
    }

    private Vector3 getLowerCenterOfCharacterController()
    {
        return new Vector3(transform.position.x, transform.position.y + con.radius, transform.position.z);
    }

    public bool isFacingWall()
    {
        float lengthMultiplier = 0.5f;

        float colliderBounds = GetComponent<CharacterController>().bounds.extents.y;
        Debug.DrawRay(getCenter(), getDirectionVector() * lengthMultiplier, Color.red, colliderBounds * lengthMultiplier);
        return Physics.Raycast(getCenter(), getDirectionVector() * lengthMultiplier, colliderBounds * lengthMultiplier, LayerMask.GetMask("Level"));
    }

    public bool isWallBehind()
    {
        float lengthMultiplier = 0.5f;

        float colliderBounds = GetComponent<CharacterController>().bounds.extents.y;
        Debug.DrawRay(getCenter(), -getDirectionVector() * lengthMultiplier, Color.red, colliderBounds * lengthMultiplier);
        return Physics.Raycast(getCenter(), -getDirectionVector() * lengthMultiplier, colliderBounds * lengthMultiplier);
    }

    private Vector3 getCenter()
    {
        return new Vector3(transform.position.x, transform.position.y + con.center.y, transform.position.z);
    }


    public void updateRotation()
    {
        if (SimpInput.getIfHeld(new string[] { "l" }))
        {
            right = false;
        }

        if (SimpInput.getIfHeld(new string[] { "r" }))
        {
            right = true;
        }
        setRotation();
    }

    public void flip()
    {
        if (!right)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        right = !right;
    }



    private void setRotation()
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
    #endregion

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    #region Combat

    public void getHit()
    {
        if (!invulnerable && currentState != stateBlocking) //if not invulnerable and not blocking
        {
            //if (isGrounded())
            if (snapToGround) //bad, but I get problems with isGrounded()
            {
                TransitionToState(stateStagger);
            }
            else
            {
                TransitionToState(stateAirHit);
            }
        }
    }

    public void throwShuriken(MainChar player)
    {
        //I THINK I SHOULD MAKE A SEPARATE SCRIPT
        //GameObject shu = Object.Instantiate(player.shuriken);
        GameObject shu = Object.Instantiate(player.shuriken, player.handPosition.position, Quaternion.identity);

        //player.prrint(shu.transform.position.x + " - " + shu.transform.position.y + " - " + shu.transform.position.z );

        //shu.transform.position = player.handPosition.position;

        GameObject enemy = getClosestEnemy(player);
        if (enemy != null)
        {
            //if there's enemy close to player and angle is in whatever range, throw towards their position

            shu.GetComponentInChildren<shurikenTrajectory>().isTracking = true;
            shu.GetComponentInChildren<shurikenTrajectory>().enemy = enemy;

        }
        else
        {
            //else throw straight forward until it hits something or timer == 1
            shu.GetComponentInChildren<shurikenTrajectory>().isTracking = false;
        }
        shu.GetComponentInChildren<shurikenTrajectory>().forward = player.right;
    }
    public GameObject getClosestEnemy(MainChar player)
    {
        Collider[] objects = Physics.OverlapSphere(player.transform.position, player.trackRadius);
        GameObject en = null;

        foreach (var o in objects)
        {
            if (o.tag.Contains("enemy"))
            {
                en = o.gameObject;
            }
        }

        return en;
    }

    public void OnCollisionEnter(Collision col)
    {
        currentState.OnCollisionEnter(this);
    }

    public bool launchAttack(Collider col, bool launches, float amount)
    {
        bool hit = false;
        //col is the attackBox of the player, c is the hitbox of the enemy
        Collider[] colliders = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Interaction"));

        foreach (Collider c in colliders)
        {
            var obj = c.gameObject;
            if (obj.tag.ToLower().Contains("enemy")) //if it's an enemy
            {
                hit = true;
                if (obj.tag.ToLower().Contains("flying"))
                {
                    obj.GetComponentInParent<AIFlyingEnemy>().getHit(false);//never launches
                }
                else if (obj.name.ToLower().Contains("ground"))
                {
                    obj.GetComponentInParent<AIGroundEnemy>().getHit(launches, amount);

                    ///if(in the air) juggle
                    ///else () stagger
                }
            }
        }
        return hit;
    }



    #endregion

    #region debug

    public void prrint(string message)
    {
        print(message);
    }

    public void setInfoText(string message)
    {
        info.text = message;
    }

    public void setOtherText(string message)
    {
        other.text = message;
    }
    #endregion

    public void playClip(AudioClip clip)
    {
        if (clip != null)
            GetComponent<AudioSource>().PlayOneShot(clip);
    }





}

/*
 
     public void shrink()
    {
        //con.height = 1.29f;
        //con.center = new Vector3(con.center.x, 1f, con.center.z);
    }

    public void unshrink()
    {
        //con.height = initHeight;
        //con.center = new Vector3(con.center.x, initCenterY, con.center.z);
    }

 */