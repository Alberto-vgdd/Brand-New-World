using UnityEngine;
using System.Collections;




//IMPORTANT:
//      
//      




//CHANGES MADE BY ALBERTO:
//     Moogba will change its direction if it collides with a "Entity" (Check Layers)
//     Moogba supports now SUPER MEGA ULTRA SLOW MOTION 
//         



public class MoogbaScript : MonoBehaviour
{
    // moogba's platform Variables 
    private float m_PlatformLength;
    private Vector3 m_PlatformCenter;

    private float m_MoogbaSpeed;
    public bool m_FacingRight;


    private Rigidbody2D m_MoogbaRigidbody;
    private Collider2D m_MoogbaCollider;
    private SpriteRenderer m_MoogbaSprite;
    
	
	void Start ()
    {
        //Variables initialization
        m_MoogbaSpeed = 25f;
        m_MoogbaRigidbody = gameObject.GetComponent<Rigidbody2D>(); 
        m_MoogbaCollider = gameObject.GetComponent<Collider2D>();
        m_MoogbaSprite = gameObject.GetComponent<SpriteRenderer>();

        //"transform.position - gameObject.GetComponent<Collider2D>().bounds.extents" calls the raycast at the end of moogba's collider.
        RaycastHit2D RayHit =Physics2D.Raycast(transform.position - m_MoogbaCollider.bounds.extents, Vector2.down, Mathf.Infinity);

        // Check if there is any platform down the enemy.
        if (RayHit.collider != null)
        {
            m_PlatformCenter = RayHit.transform.position;
            m_PlatformLength = RayHit.collider.bounds.extents.x;
        }
        //If there's no platform down the moogba, the moogba will be destroyed.
        else
        {
            Destroy(gameObject, 0.05f);
        }

        //Set Initial m_MoogbaSpeed And Sprite Direction; 
        if (!m_FacingRight)
        {
            m_MoogbaSprite.flipX = !m_FacingRight;
            m_MoogbaSpeed *= -1;
        }

    }


    // Update is called once per frame
    void FixedUpdate ()
   {
      if ((((transform.position.x + m_MoogbaCollider.bounds.extents.x) >= (m_PlatformCenter.x + m_PlatformLength)) && m_FacingRight) || (((transform.position.x - m_MoogbaCollider.bounds.extents.x) <= (m_PlatformCenter.x - m_PlatformLength)) && !m_FacingRight))
      {
            ChangeMovementDirection();
       }


        m_MoogbaRigidbody.velocity = new Vector2(m_MoogbaSpeed*Time.fixedDeltaTime, m_MoogbaRigidbody.velocity.y);
    }

    void ChangeMovementDirection()
    {
        m_MoogbaSprite.flipX = m_FacingRight;
        m_FacingRight = !m_FacingRight;
        m_MoogbaSpeed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Fireball"))
        {
            Destroy(gameObject, 0.05f);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Entities")) 
        {
            if ((collision.transform.position.x <= m_MoogbaRigidbody.position.x && !m_FacingRight) || (collision.transform.position.x >= m_MoogbaRigidbody.position.x && m_FacingRight))
            {
                ChangeMovementDirection();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entities"))
        {
            if ((collision.transform.position.x <= m_MoogbaRigidbody.position.x && !m_FacingRight) || (collision.transform.position.x >= m_MoogbaRigidbody.position.x && m_FacingRight))
            {
                ChangeMovementDirection();
            }
        }
    }
}
