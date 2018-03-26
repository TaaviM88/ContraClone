using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float inputDirection; // x value of our movevector
    private float verticalVelocity; // Y value of our move vector

    public float jumpForce = 10.0f;
    public float speed = 5.0f;
    public float gravity = 1.0f;
    public float hoverHeight = 0.2f;
    private bool secondJumpAvail = false;
    private bool _facingRight = true;
    public float value;

    public Vector2 initialPos;

    bool isRunning;
    //bool CanPlayerMove;


    //AnimeController _animeScript;
    //private Animator anime;
    private Rigidbody rigidbody;
    private Vector2 moveVector;
    //lastmotionilla lukittiin hypyn suunta
    private Vector2 lastMotion;
    private CharacterController controller;

    /*Animaatio STATET---------------------------------------------
	 * State 0 = Ase stance(idle)
	 * State 1 = Run with hands
	 * State 2 = Jump Up
	 * State 3 = Falling
	 * State 4 = attackk
	 * State 5 = walljump
	 *--------------------------------------------------------------
	*/
    // Use this for initialization

    void Awake()
    {
        //DisablePlayerMovement();
    }

    void Start()
    {
        initialPos = transform.position;
        controller = GetComponent<CharacterController>();
        //anime = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z != 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            Debug.Log ("Moved player");
        }

        //if (CanPlayerMove == true)
        //{

            //LIIKKUMINEN JA FLIP, Run ja Idle animaatio ----------------------------------------------------------------
            IsControllerGrounded();
            moveVector = Vector2.zero;
            inputDirection = Input.GetAxis("P1movement") * speed;
            value = Input.GetAxis("P1movement");
            Debug.Log("Nappia painetaan");
            if (value > 0)
            {

                if (_facingRight == false)
                {
                    Flip();
                }
                if (IsControllerGrounded())
                {
                    isRunning = true;
                    //anime.SetInteger("State", 1);
                }
            }

            if (value < 0)
            {
                if (_facingRight == true)
                {
                    Flip();
                }
                if (IsControllerGrounded())
                {
                    isRunning = true;
                    //anime.SetInteger("State", 1);
                }
            }
            if (value == 0 && verticalVelocity == 0)
            {
                isRunning = false;
                //anime.SetInteger("State", 0);
            }

            //----------------------------------------------------------------------------

            //HYÖKKÄYS ANIMAATIO (hyökkäys komento itsessään on playeruseskill.cs)

            /*if (Input.GetButtonDown("P1Fire"))
            {
                //anime.SetInteger("State", 4);
                //AudioManager.audioManager.WandSwing();
            }*/

            //----------------------------------------------------------------------------
            //HYPPY, TUPLAHYPPY ja molempien animaatiot
            if (IsControllerGrounded())
            {
                verticalVelocity = 0;

                if (Input.GetButtonDown("P1Jump"))
                {
                    //anime.SetInteger("State", 2);
                    verticalVelocity = jumpForce;
                    //Kun ilmassa secondjump on aktiivinen
                    secondJumpAvail = true;
                }
                moveVector.x = inputDirection;
            }
            else
            {

                if (Input.GetButtonDown("P1Jump"))
                {
                    if (secondJumpAvail)
                    {
                        //anime.SetInteger("State", 2);
                        verticalVelocity = jumpForce;
                        secondJumpAvail = false;
                    }
                }

                verticalVelocity -= gravity * Time.deltaTime;
                //Jos haluat vapaan liikkumisen ja vapaan hyppy suunnan, ota kaksi seuraavaa käyttöön
                moveVector.x = inputDirection;
                moveVector.y = inputDirection;
            }

            moveVector.y = verticalVelocity;
            //  moveVector = new Vector3(inputDirection, verticalVelocity, 0);
            controller.Move(moveVector * Time.deltaTime);
            lastMotion = moveVector;


        }

   // }
    //-----------------------------------------------------------------------------------------------

    void FixedUpdate()
    {
        //tässä FALLING animaation laukaisu rigidbodylla

        //Debug.Log(moveVector.y);
        if (moveVector.y != 0f /*&& Input.GetButtonUp("P1Jump")*/)
        {
            // Debug.Log("hyppasin, menen up ja hyppy ei pohjassa");
            //anime.SetInteger("State", 3);

        }
    }
    //-------------------------------------------------------------------------------------

    // GROUNDED CHECK RAYCASTILLA
    // Raycast grounded check https://www.youtube.com/watch?v=8Cado6CzZUA&list=PLLH3mUGkfFCWwekOW1OMxyyIgc-Qm1OhI&index=10
    private bool IsControllerGrounded()
    {

        Vector2 leftRayStart;
        Vector2 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;
        rightRayStart.x += controller.bounds.extents.x;

        Debug.DrawRay(leftRayStart, Vector2.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector2.down, Color.blue);

        if (Physics.Raycast(rightRayStart, Vector2.down, (controller.height / 2) + hoverHeight))
        {
            //Debug.Log("osuu varpaat");
            return true;
        }

        if (Physics.Raycast(leftRayStart, Vector2.down, (controller.height / 2) + hoverHeight))
        {
            //Debug.Log("osuu kantapaa");
            return true;
        }

        //Debug.Log("ei osu maahan");
        return false;
    }
    
    //------------------------------------------------------------------------------------

    //TÄSSÄ WALLJUMP JA SEN ANIMAATIO
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if (CanPlayerMove == true)
        //{
            if (controller.collisionFlags == CollisionFlags.Sides)
            {
                if (Input.GetButtonDown("P1Jump"))
                {
                    //anime.SetInteger("State", 5);
                    moveVector = hit.normal * speed;
                    verticalVelocity = jumpForce;
                }

            }
       // }
    }
    //--------------------------------------------------------------------------------------

    //TÄSSÄ MÄÄRITELTIIN FLIPPI
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector2 theScale = transform.localScale;
        //theScale.z *= -1;
        transform.localScale = theScale;
    }

    /*public void EnablePlayerMovement()
    {
        CanPlayerMove = true;
        //pelaaja voi liikkua
        //Debug.Log("pelaaja1 voi liikkua");
    }
    public void DisablePlayerMovement()
    {
        CanPlayerMove = false;
        //pelaaja ei voi liikkua
        //Debug.Log("pelaaja1 ei voi liikkua");
    }
    */
}
