using UnityEngine;
using System.Collections;



//IMPORTANT:
//      Every Single platform type requires now a different Physics2D material. (Player may stick on walls, ask Alberto for more details)
//      Diferent enemies may also need one of those materials


public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D m_PlayerRigidbody;
    private Animator m_PlayerAnimator;
    private FeetControllerScript m_PlayerFeetScript;
    private PlayerShootScript m_PlayerShootScript;

    //These values change how fast/high  does the player run/jump
    public float m_WalkSpeedValue;
    public float m_RunSpeedValue;
    public float m_JumpSpeedValue;

    //this variable manages the amount of horizontal velocity that the player is given
    private float m_HorizontalVelocity;

    //Variables to manage jumping & falling.
    private bool m_IsOnGround;
    private bool m_IsJumping;

    private float m_WalkJumpBoost;
    private float m_RunJumpBoost;

    //Variables to reverse animation speed, and change between movement animations
    private bool m_IsFacingRight;
    private bool m_IsRunning;
    private float m_IsWalking; // 3 different states ---> -1, 0, 1




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

        m_WalkSpeedValue = 0.75f;
        m_RunSpeedValue = 0.75f;
        m_JumpSpeedValue = 2.25f;

        m_WalkJumpBoost = 1.1f;
        m_RunJumpBoost = 1.25f;

    }

    // Update is called once per frame
    void Update ()
    {
        //Walking
        m_IsWalking = Input.GetAxis("MovementAxisX");

        //Running
        if(Input.GetButton("Run") == true) { m_IsRunning = true; } else { m_IsRunning = false; }

        //Jumping
        if (Input.GetButton("Jump") == true  && m_IsOnGround == true) { m_IsJumping = true; } else { m_IsJumping = false; }

        //m_PlayerRigidbody.transform.Translate(new Vector2(0.1f,0.1f) * Time.fixedDeltaTime);
	}

    //FixedUpdate is called every frame in Time.DeltaTime
    void FixedUpdate() 
    {
        CheckOnGround();
        CheckFacing();
        Move();
        Jump();   
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
        ChangeAnimationSpeed();

        //Change animations.
         ChangeAnimation();

        //Set the Speed
         m_PlayerRigidbody.velocity = new Vector2(m_HorizontalVelocity, m_PlayerRigidbody.velocity.y);

    }

    void ChangeAnimationSpeed()
    {
        if (((m_IsFacingRight && m_IsWalking < 0) || (!m_IsFacingRight && m_IsWalking > 0)))
        {
            m_PlayerAnimator.SetFloat("Speed", -1f);
        }
        else
        {
            m_PlayerAnimator.SetFloat("Speed", 1f);
        }        
    }
    void ChangeAnimation()
    {
        if (m_IsWalking !=0)
        {
            m_PlayerAnimator.SetBool("Walk", true);
        }
        else
        {
            m_PlayerAnimator.SetBool("Walk", false);
        }
        
    }

    void SetHorizontalVelocity()
    {
            m_HorizontalVelocity = m_WalkSpeedValue; 

            if (m_IsRunning && m_IsWalking != 0) 
            {
                m_HorizontalVelocity += m_RunSpeedValue;
                m_PlayerAnimator.speed = 1.5f;
            }
            else
            {
                m_PlayerAnimator.speed = 1f;
            }

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
