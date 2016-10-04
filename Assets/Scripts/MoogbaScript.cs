using UnityEngine;
using System.Collections;

public class MoogbaScript : MonoBehaviour
{
    // moogba's platform Variables 
    private Collider2D m_PlatformCollider;
    private float m_PlatformLength;
    private Vector3 m_PlatformCenter;

    private float m_MoogbaSpeed;
    private bool m_FacingRight;


    private Rigidbody2D m_MoogbaRigidbody;
    private Collider2D m_MoogbaCollider;
    private SpriteRenderer m_MoogbaSprite;
    
	
	void Start ()
    {
        //Variables initialization
        m_FacingRight = false;
        m_MoogbaSpeed = .5f;
        m_MoogbaRigidbody = gameObject.GetComponent<Rigidbody2D>(); 
        m_MoogbaCollider = gameObject.GetComponent<Collider2D>();
        m_MoogbaSprite = gameObject.GetComponent<SpriteRenderer>();

        //"transform.position - gameObject.GetComponent<Collider2D>().bounds.extents" calls the raycast at the end of moogba's collider.
        RaycastHit2D RayHit =Physics2D.Raycast(transform.position - m_MoogbaCollider.bounds.extents, Vector2.down, Mathf.Infinity);

        // Check if there is any platform down the enemy.
        if (RayHit.collider != null)
        {
            m_PlatformCollider = RayHit.collider;
            m_PlatformCenter = RayHit.transform.position;
            m_PlatformLength = RayHit.collider.bounds.extents.x;
            
        }
        //If there's no platform down the moogba, the moogba will be destroyed.
        else
        {
            Destroy(gameObject, 0.05f);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
   {
      if (((transform.position.x + m_MoogbaCollider.bounds.extents.x) >= (m_PlatformCenter.x + m_PlatformLength)) || ((transform.position.x - m_MoogbaCollider.bounds.extents.x) <= (m_PlatformCenter.x - m_PlatformLength)))
      {
            ChangeMovementDirection();
       }


        m_MoogbaRigidbody.velocity = new Vector2(m_MoogbaSpeed, m_MoogbaRigidbody.velocity.y);
    }

    void ChangeMovementDirection()
    {
        m_FacingRight = !m_FacingRight;
        m_MoogbaSprite.flipX = m_FacingRight;
        m_MoogbaSpeed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Fireball"))
        {
            Destroy(gameObject, 0.05f);
        }
    }
}
