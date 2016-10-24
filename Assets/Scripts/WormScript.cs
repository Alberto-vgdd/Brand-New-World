using UnityEngine;
using System.Collections;





public class WormScript : MonoBehaviour
{
    // Worm's platform Variables 
    private float m_PlatformLength;
    private Vector3 m_PlatformCenter;

    private float m_WormSpeed;
    public bool m_FacingRight;


    private Rigidbody2D m_WormRigidbody;
    private BoxCollider2D m_WormCollider;
    private SpriteRenderer m_WormSpriteRenderer;


    void Start ()
    {
        //Variables initialization
        m_WormSpeed = 15f;
        m_WormRigidbody = gameObject.GetComponent<Rigidbody2D>(); 
        m_WormCollider = gameObject.GetComponent<BoxCollider2D>();
        m_WormSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //"transform.position - gameObject.GetComponent<Collider2D>().bounds.extents" calls the raycast at the end of Worm's collider.
        RaycastHit2D RayHit = Physics2D.Raycast(transform.position - m_WormCollider.bounds.extents, Vector2.down, Mathf.Infinity);

        // Check if there is any platform down the enemy.
        if (RayHit.collider != null)
        {
            m_PlatformCenter = RayHit.transform.position;
            m_PlatformLength = RayHit.collider.bounds.extents.x;
        }
        //If there's no platform down the Worm, the Worm will be destroyed.
        else
        {
            Destroy(gameObject, 0.05f);
        }

        //Set Initial m_WormSpeed And Sprite Direction; 
        if (!m_FacingRight)
        {
            m_WormSpeed *= -1;
        }

    }


    // Update is called once per frame
    void FixedUpdate ()
   {
        ChangeColliderSize();
      if ((((transform.position.x + m_WormCollider.bounds.extents.x) >= (m_PlatformCenter.x + m_PlatformLength)) && m_FacingRight) || (((transform.position.x - m_WormCollider.bounds.extents.x) <= (m_PlatformCenter.x - m_PlatformLength)) && !m_FacingRight))
      {
            ChangeMovementDirection();
       }


        m_WormRigidbody.velocity = new Vector2(m_WormSpeed*Time.fixedDeltaTime,  m_WormRigidbody.velocity.y);
    }

    void ChangeMovementDirection()
    {
        m_FacingRight = !m_FacingRight;
        m_WormSpeed *= -1;
    }

    void ChangeColliderSize()
    {
        //Set sprite's size as collider's size. You have to multiply the inversed local scale, otherwise the local scale will be applied again in the collider.
        m_WormCollider.size = new Vector2(m_WormSpriteRenderer.bounds.size.x * 1f / transform.localScale.x, m_WormSpriteRenderer.bounds.size.y * 1f / transform.localScale.y);
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
