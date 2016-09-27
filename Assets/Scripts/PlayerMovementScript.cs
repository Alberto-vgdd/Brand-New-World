using UnityEngine;
using System.Collections;



//TO DO:

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D m_PlayerRigidbody;
    private Animator m_PlayerAnimator;
    //private SpriteRenderer m_PlayerSprite;
    private FeetControllerScript m_PlayerFeetScript;
    private PlayerShootScript m_PlayerShootScript;

    //These values change how fast/high  does the player run/jump
    public float m_WalkSpeed;
    public float m_JumpSpeed;
    public float m_RunMultiplier;

    //This manages the position displacement on X axis
    private float m_Displacement;

    //Variables to manage jumping & falling.
    private bool m_IsOnGround;
    private bool m_IsJumping;

    private float m_WalkJumpBoost;
    private float m_RunJumpBoost;

    //Variables to reverse animation speed, and change between movement animations
    private bool m_IsFacingRight;
    private bool m_IsMoving;
    private bool m_IsRunning;



	// Use this for initialization
	void Start ()
    {
        InitializeVariables();

    }
	
    void InitializeVariables()
    {
        m_PlayerRigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponent<Animator>();
        //m_PlayerSprite = GetComponent<SpriteRenderer>();
        m_PlayerFeetScript = transform.FindChild("Feet").GetComponent<FeetControllerScript>();
        m_PlayerShootScript = transform.GetComponent<PlayerShootScript>();

        m_Displacement = 0f;

        m_IsMoving = false;
        m_IsJumping = false;
        m_IsOnGround = false;
        m_IsFacingRight = true;

        m_WalkJumpBoost = 1.1f;
        m_RunJumpBoost = 1.25f;

        

        

    }

    // Update is called once per frame
    void Update ()
    {
        //Walking
        m_Displacement = (Input.GetAxis("MovementAxisX") * m_WalkSpeed * Time.fixedDeltaTime);
        if (m_Displacement != 0){ m_IsMoving = true; } else { m_IsMoving = false; }

        //Running
        if(Input.GetButton("Run") == true) { m_IsRunning = true; } else { m_IsRunning = false; }

        //Jumping
        if (Input.GetButton("Jump") == true  && m_IsOnGround == true) { m_IsJumping = true; } else { m_IsJumping = false; }
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
        //In case the player is running, double the displacement and accelerate the animation
        ChangeSpeed();

        //Check if player has to move forward or backward, then change animation speed
        ChangeAnimationSpeed();

        //Change animations.
         ChangeAnimation();

        m_PlayerRigidbody.position += new Vector2(m_Displacement, 0);
    }

    void ChangeAnimationSpeed()
    {
        if (((m_IsFacingRight && m_Displacement < 0) || (!m_IsFacingRight && m_Displacement > 0)))
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
        //Change this by checking GetAxis.
        m_PlayerAnimator.SetBool("Walk", m_IsMoving);
    }
    void ChangeSpeed()
    {
        if (m_IsRunning)
        {
            m_Displacement *= m_RunMultiplier;
            m_PlayerAnimator.speed = 1.5f;
        }
        else
        {
            m_PlayerAnimator.speed = 1f;
        }

    }

    void Jump()
    {
        if (m_IsJumping == true)
        {
            if (m_IsRunning && m_IsMoving)
            {
                m_PlayerRigidbody.velocity = new Vector2(m_PlayerRigidbody.velocity.x, m_JumpSpeed * m_RunJumpBoost);
            }
            else if (m_IsMoving)
            {
                m_PlayerRigidbody.velocity = new Vector2(m_PlayerRigidbody.velocity.x, m_JumpSpeed * m_WalkJumpBoost);
            }
            else
            {
                m_PlayerRigidbody.velocity = new Vector2(m_PlayerRigidbody.velocity.x, m_JumpSpeed);
            }

        }
    }


   

    
}
