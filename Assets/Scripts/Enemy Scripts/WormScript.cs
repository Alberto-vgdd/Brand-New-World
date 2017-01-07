using UnityEngine;
using System.Collections;





public class WormScript : MonoBehaviour
{
    // Worm's platform Variables 
    private float m_PlatformLength;
    private Vector3 m_PlatformCenter;

    private float m_WormSpeed;
    public bool m_FacingRight;

    private bool m_IsFalling;
    private float m_MaximunFallingTime;
    private float m_ElapsedFallingTime;


    private Rigidbody2D m_WormRigidbody;
    private BoxCollider2D m_WormCollider;
    private SpriteRenderer m_WormSpriteRenderer;


    void Start ()
    {
        //Variables initialization
        m_WormSpeed = 0.3f;
        m_WormRigidbody = gameObject.GetComponent<Rigidbody2D>(); 
        m_WormCollider = gameObject.GetComponent<BoxCollider2D>();
        m_WormSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        m_IsFalling = true;
        m_ElapsedFallingTime = 0.0f;
        m_MaximunFallingTime = 1.5f;

        //Set Initial m_WormSpeed And Sprite Direction; 
        if (!m_FacingRight)
        {
            m_WormSpeed *= -1;
        }
    }



    void FixedUpdate()
    {
        //Set sprite's size to the collider.
        ChangeColliderSize();

        if (GlobalDataScript.INPUT_ENABLED)
        {
            //Worm's Main Function.
            WormAI();

            //Move the worm
            m_WormRigidbody.velocity = new Vector2(m_WormSpeed,  m_WormRigidbody.velocity.y);
        }
        else
        {
            m_WormRigidbody.velocity = new Vector2(0f,m_WormRigidbody.velocity.y);
        }
    }

    void ChangeMovementDirection()
    {
        m_FacingRight = !m_FacingRight;
        m_WormSpeed *= -1;
    }

    void ChangeColliderSize()
    {
        //Set sprite's size as collider's size. You have to multiply the inversed local scale, otherwise the local scale will be applied again in the collider.
        m_WormCollider.size = new Vector2(m_WormSpriteRenderer.bounds.size.x * 1f / transform.localScale.x, m_WormCollider.size.y);
    }

    void WormAI()
    {
        if (!m_IsFalling)
        {
            //check if worm is about to fall, the turn worm around.
            if ((((transform.position.x + m_WormCollider.bounds.extents.x) >= (m_PlatformCenter.x + m_PlatformLength)) && m_FacingRight) || (((transform.position.x - m_WormCollider.bounds.extents.x) <= (m_PlatformCenter.x - m_PlatformLength)) && !m_FacingRight))
            {
                ChangeMovementDirection();

                //If worm has fallen, find a new platform for it.
                if (transform.position.y < m_PlatformCenter.y)
                {
                    m_IsFalling = true;
                    m_ElapsedFallingTime = 0.0f;
                }
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Fireball"))
        {
            Destroy(gameObject, 0.05f);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Entities")) 
        {
            if ((collision.transform.position.x <= m_WormRigidbody.position.x && !m_FacingRight) || (collision.transform.position.x >= m_WormRigidbody.position.x && m_FacingRight))
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
            else if ((collision.transform.position.x <= m_WormRigidbody.position.x && !m_FacingRight) || (collision.transform.position.x >= m_WormRigidbody.position.x && m_FacingRight))
            {
                ChangeMovementDirection();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entities"))
        {
            if ((collision.transform.position.x <= m_WormRigidbody.position.x && !m_FacingRight) || (collision.transform.position.x >= m_WormRigidbody.position.x && m_FacingRight))
            {
                ChangeMovementDirection();
            }
        }
    }
}
