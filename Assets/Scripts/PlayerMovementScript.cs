using UnityEngine;
using System.Collections;





public class PlayerMovementScript : MonoBehaviour
{
    //Components attached to the player that are needed here.
    private Rigidbody2D m_PlayerRigidbody;
    private Animator m_PlayerAnimator;
    private FeetControllerScript m_PlayerFeetScript;
    private PlayerShootScript m_PlayerShootScript;

    //These values change how fast/high  does the player run/jump
    public float m_WalkSpeedValue;
    public float m_RunSpeedValue;
    public float m_JumpSpeedValue;

    //How  walking/running affects  the jump.
    private float m_WalkJumpBoost;
    private float m_RunJumpBoost;


    //this variable manages the amount of horizontal velocity that the player is given
    private float m_HorizontalVelocity;

    //Variables to manage jumping & falling.
    private bool m_IsOnGround;
    private bool m_IsJumping;

    //Variables to reverse animation speed, and change between movement animations
    private bool m_IsFacingRight;
    private bool m_IsRunning;
    private float m_IsWalking; // 3 different states ---> -1, 0, 1
    private bool crouching;




    // Use this for initialization
    void Start ()
    {
        InitializeVariables();

    }
	
    void InitializeVariables()
    {
        m_PlayerRigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponent<Animator>();
        m_PlayerFeetScript = transform.FindChild("Feet").GetComponent<FeetControllerScript>();
        m_PlayerShootScript = transform.GetComponent<PlayerShootScript>();


        m_IsJumping = false;
        m_IsOnGround = false;
        m_IsFacingRight = true;
        m_IsWalking = 0f;

        m_WalkSpeedValue = 1f;
        m_RunSpeedValue = 1f;
        m_JumpSpeedValue = 3f;


        m_WalkJumpBoost = 1.1f;
        m_RunJumpBoost = 1.25f;

    }

    // Update is called once per frame. Read any input here.
    void Update ()
    {
        //Walking
        m_IsWalking = Input.GetAxis("MovementAxisX");

        //Running
        if(Input.GetButton("Run") == true) { m_IsRunning = true; } else { m_IsRunning = false; }

        //Jumping
        if (Input.GetButton("Jump") == true  && m_IsOnGround == true) { m_IsJumping = true; } else { m_IsJumping = false; }
	}

    //FixedUpdate is called every frame in Time.fixedDeltaTime, physics related stuff here.
    void FixedUpdate() 
    {
        //This function updates m_IsOnGround, to enable or not jumping.
        CheckOnGround();

        //This funciton updates where the player should be looking at  based on  mouse position.
        CheckFacing();

        CheckCrouching();


        //Horizontal Speed based on inputs
        Move();

        //Vertical Speed based on inputs
        Jump();


    }


    private void CheckCrouching()
    {
        crouching = m_PlayerShootScript.GetCrouching();
    }

    void CheckOnGround()
    {
        m_IsOnGround = m_PlayerFeetScript.GetOnGround();
    }

    void CheckFacing()
    {
        m_IsFacingRight = m_PlayerShootScript.GetFacing();
    }

    void Move()
    {
        //Velocity in case either m_IsWalking or m_IsRunning are true
        SetHorizontalVelocity();

        //Check if player has to move forward or backward (it depends on the mouse position), then change animation speed
        FlipPlayerAnimation();

        //Change animations.
         ChangeAnimation();

        //Set the Speed
         m_PlayerRigidbody.velocity = new Vector2(m_HorizontalVelocity, m_PlayerRigidbody.velocity.y);

    }

    void FlipPlayerAnimation()
    {
        if (((m_IsFacingRight && m_IsWalking < 0) || (!m_IsFacingRight && m_IsWalking > 0)))
        {
            m_PlayerAnimator.SetFloat("Speed", m_PlayerAnimator.GetFloat("Speed") * -1f);
        }
        else
        {
            m_PlayerAnimator.SetFloat("Speed", m_PlayerAnimator.GetFloat("Speed") * 1f);
        }        
    }
    void ChangeAnimation()
    {
        if (m_IsWalking != 0)
        {
            m_PlayerAnimator.SetBool("Walk", true);
        }
        else
        {
            m_PlayerAnimator.SetBool("Walk", false);
        }

        if (crouching)
            m_PlayerAnimator.SetBool("Crouch", true);
        else
            m_PlayerAnimator.SetBool("Crouch", false);
        
    }

    void SetHorizontalVelocity()
    {
            m_HorizontalVelocity = m_WalkSpeedValue; 

            if (m_IsRunning) 
            {
                m_HorizontalVelocity += m_RunSpeedValue;
            }

        
        m_PlayerAnimator.SetFloat("Speed", m_HorizontalVelocity);
        m_HorizontalVelocity *= m_IsWalking;
    }

    void Jump()
    {
        if (m_IsJumping == true)
        {
            if (m_IsRunning && m_IsWalking !=0)
            {
                m_PlayerRigidbody.velocity = new Vector2(m_PlayerRigidbody.velocity.x, m_JumpSpeedValue * m_RunJumpBoost);
            }
            else if (m_IsWalking != 0)
            {
                m_PlayerRigidbody.velocity = new Vector2(m_PlayerRigidbody.velocity.x, m_JumpSpeedValue * m_WalkJumpBoost);
            }
            else
            {
                m_PlayerRigidbody.velocity = new Vector2(m_PlayerRigidbody.velocity.x, m_JumpSpeedValue);
            }

        }
    }






}
