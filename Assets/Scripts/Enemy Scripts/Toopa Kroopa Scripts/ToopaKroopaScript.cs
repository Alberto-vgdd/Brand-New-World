using UnityEngine;
using System.Collections;





public class ToopaKroopaScript : MonoBehaviour
{
    // Worm's platform Variables 
    private float m_PlatformLength;
    private Vector3 m_PlatformCenter;

    private float m_KoopaTroopaSpeed;
    public bool m_IsFacingRight;


    private float m_IsMoving;
    private float m_MaximunIdleTime;
    private float m_ElapsedIdleTime;
   

    private bool m_IsFalling;
    private float m_MaximunFallingTime;
    private float m_ElapsedFallingTime;

    private bool m_IsShooting;
    private bool m_IsBulletShoot;
    private float m_Time2Shoot;
    private float m_ElapsedShootTime;



    private Rigidbody2D m_KoopaTroopaRigidbody;
    private BoxCollider2D m_KoopaTroopaCollider;
    private SpriteRenderer m_KoopaTroopaSpriteRenderer;
    private Animator m_ToopaKroopaAnimator;

    public Transform m_TargetTransform;
    private Rigidbody2D m_BulletClone;
    public Rigidbody2D m_BulletPrefab;
    private Vector2 m_TargetMaximunDistance;
    private float m_BulletForce;




    void Start()
    {
        //Variables initialization
        m_KoopaTroopaSpeed = 12.5f;
        
        m_KoopaTroopaRigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_KoopaTroopaCollider = gameObject.GetComponent<BoxCollider2D>();
        m_KoopaTroopaSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_ToopaKroopaAnimator = gameObject.GetComponent<Animator>();

        m_IsFalling = true;
        m_ElapsedFallingTime = 0.0f;
        m_MaximunFallingTime = 1.5f;

        m_IsMoving = 1.0f;
        m_MaximunIdleTime = 2f;
        m_ElapsedIdleTime = 0f;

        m_IsShooting = false;
        m_IsBulletShoot = false;
        m_Time2Shoot = 2f;
        m_ElapsedShootTime= 0.0f;
        m_TargetMaximunDistance =new Vector2( 1.5f, 0.15f);
        m_BulletForce = 5f;

        if (!m_IsFacingRight)
        {
            m_IsMoving *= -1;
        }



    }



    void FixedUpdate()
    {
        //Set sprite's size to the collider.
        ChangeColliderSize();

        if (GlobalDataScript.INPUT_ENABLED)
        {
            //KoopaTroopa's Main Function.
            KoopaTroopaAI();

            //Change KoopaTroopa's Animation
            ChangeAnimation();
        }
        else
        {
            m_KoopaTroopaRigidbody.velocity = new Vector2(0f, m_KoopaTroopaRigidbody.velocity.y);
        }
    }

    void ChangeMovementDirection()
    {
        m_IsFacingRight = !m_IsFacingRight;
        m_IsMoving = 1;

        if (!m_IsFacingRight)
        {
            m_IsMoving *= -1;
        }
    }

    void ChangeColliderSize()
    {
        //Set sprite's size as collider's size. You have to multiply the inversed local scale, otherwise the local scale will be applied again in the collider.
        m_KoopaTroopaCollider.size = new Vector2(m_KoopaTroopaSpriteRenderer.bounds.size.x * 0.3f / transform.localScale.x, m_KoopaTroopaSpriteRenderer.bounds.size.y * 0.9f / transform.localScale.y);
    }
    
    void ChangeAnimation()
    {
        m_ToopaKroopaAnimator.SetBool("FacingRight", m_IsFacingRight);

        if (m_IsMoving == 0)
        {
            m_ToopaKroopaAnimator.SetBool("Moving", false);
        }
        else
        {
            m_ToopaKroopaAnimator.SetBool("Moving", true);
        }

        m_ToopaKroopaAnimator.SetBool("Shooting", m_IsShooting);

    }

    void KoopaTroopaAI()
    {
        //KoopaTroopa can only shoot, walk and idle when isn't falling.
        if (!m_IsFalling)
        {
            if (m_IsShooting)
            {
                m_ElapsedShootTime += Time.fixedDeltaTime;
                if (m_ElapsedShootTime > m_Time2Shoot*1/2)
                {
                    if (!m_IsBulletShoot)
                    {
                        m_IsBulletShoot = true;
                        ShootBullet();
                    }
                    if (m_ElapsedShootTime > m_Time2Shoot)
                    {
                        m_IsShooting = false;
                        m_IsMoving = 1.0f;
                        if (!m_IsFacingRight)
                        {
                            m_IsMoving *= -1;
                        }
                    }
                }
                


            }
            else
            {
                //Move KoopaTroopa until it arrives the platform's corner
                if (m_IsMoving != 0)
                {
                    //check if KoopaTroopa is in the corner, then stop moving
                    if ((((transform.position.x + m_KoopaTroopaCollider.bounds.extents.x) >= (m_PlatformCenter.x + m_PlatformLength)) && m_IsFacingRight) || (((transform.position.x - m_KoopaTroopaCollider.bounds.extents.x) <= (m_PlatformCenter.x - m_PlatformLength)) && !m_IsFacingRight))
                    {
                        m_IsMoving = 0;
                        m_ElapsedIdleTime = 0.0f;
                    }
                }

                //Wait 2s until start moving again.
                if (m_IsMoving == 0)
                {
                    m_ElapsedIdleTime += Time.fixedDeltaTime;
                    if (m_ElapsedIdleTime >= m_MaximunIdleTime)
                    {
                        ChangeMovementDirection();
                    }
                }

                if (TargetAvailable())
                {
                    m_IsMoving = 0f;
                    m_IsShooting = true;

                    InstantiateBullet();


                } 
            }




            //If KoopaTroopa has fallen, find a new platform for it.
            if (transform.position.y < m_PlatformCenter.y)
            {
                m_IsFalling = true;
                m_ElapsedFallingTime = 0.0f;
            }
        }

        //If it is falling, wait until worm finds a new platform before 1.5s, if there isn't a new platform,  destroy the gameobject.
        else
        {
            m_ElapsedFallingTime += Time.fixedDeltaTime;
            if (m_ElapsedFallingTime >= m_MaximunFallingTime)
            {
                Destroy(gameObject, 0.5f);
            }
        }

        m_KoopaTroopaRigidbody.velocity = new Vector2(m_IsMoving * m_KoopaTroopaSpeed * Time.fixedDeltaTime, m_KoopaTroopaRigidbody.velocity.y);

    }

    void InstantiateBullet()
    {
        if (m_IsFacingRight)
        {
            m_BulletClone = Instantiate(m_BulletPrefab,transform.position + new Vector3(1.6f*transform.localScale.x,2f*transform.localScale.y, 0f), transform.rotation) as Rigidbody2D;
        }
        else
        {
            m_BulletClone = Instantiate(m_BulletPrefab, transform.position + new Vector3(-1.6f*transform.localScale.x, 2f*transform.localScale.y, 0f), transform.rotation) as Rigidbody2D;
        }

        m_IsBulletShoot = false;
        m_ElapsedShootTime = 0.0f;
    }

    void ShootBullet()
    {
        if (m_IsFacingRight)
        {
            m_BulletClone.AddForce(Vector2.right * m_BulletForce);
        }
        else
        {
            m_BulletClone.AddForce(Vector2.left * m_BulletForce);
        }
        
    }

    bool TargetAvailable()
    {
        if ((m_IsFacingRight  &&  m_TargetTransform.position.x >= transform.position.x) || (!m_IsFacingRight && m_TargetTransform.position.x <= transform.position.x))
        {
            if (Mathf.Abs(m_TargetTransform.position.x - transform.position.x) < m_TargetMaximunDistance.x)
            {
                if (Mathf.Abs(m_TargetTransform.position.y - transform.position.y - (2f * transform.localScale.y)) < m_TargetMaximunDistance.y)
                {
                    return true;
                }
               return false;
            }
            return false;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Iceball"))
        {
            Destroy(gameObject, 0.05f);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Entities"))
        {
            if ((collision.transform.position.x <= m_KoopaTroopaRigidbody.position.x && !m_IsFacingRight) || (collision.transform.position.x >= m_KoopaTroopaRigidbody.position.x && m_IsFacingRight))
            {
                ChangeMovementDirection();
            }
        }

        
        if (collision.transform.CompareTag("Platform"))
        {
            if (m_IsFalling)
            {
                m_PlatformCenter = collision.transform.position;
                m_PlatformLength = collision.collider.bounds.extents.x;

                m_IsFalling = false;
            }
             else if ((collision.transform.position.x <= m_KoopaTroopaRigidbody.position.x && !m_IsFacingRight) || (collision.transform.position.x >= m_KoopaTroopaRigidbody.position.x && m_IsFacingRight))
            {
                ChangeMovementDirection();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entities"))
        {
            if ((collision.transform.position.x <= m_KoopaTroopaRigidbody.position.x && !m_IsFacingRight) || (collision.transform.position.x >= m_KoopaTroopaRigidbody.position.x && m_IsFacingRight))
            {
                ChangeMovementDirection();
            }
        }
    }
}