using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{
    private SpriteRenderer m_PlayerSpriteRenderer;
    private Rigidbody2D m_PlayerRigidbody;
    private BoxCollider2D m_PlayerBoxCollider;
    private BoxCollider2D m_PlayerFeetCollider;

    private float m_PlayerHeight;
    private bool m_IsOnGround;
    public int punt;

    void Start()
    {
        m_PlayerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_PlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_PlayerBoxCollider = gameObject.GetComponent<BoxCollider2D>();
        m_PlayerFeetCollider = transform.FindChild("Feet").GetComponent<BoxCollider2D>();

        m_PlayerHeight = m_PlayerSpriteRenderer.bounds.size.y / transform.localScale.y;
}

    void FixedUpdate()
    {
        //Set sprite's size to the collider. Also place
        ChangePlayerColliderSize();
        punt = GlobalDataScript.currentPuntuation;
    }

    void ChangePlayerColliderSize()
    {
        

        //Set sprite's size as collider's size. You have to multiply the inversed local scale, otherwise the local scale will be applied again in the collider.
        m_PlayerBoxCollider.size = new Vector2(m_PlayerBoxCollider.size.x, m_PlayerSpriteRenderer.bounds.size.y/transform.localScale.y );

        m_PlayerBoxCollider.offset = new Vector2(0f, (-m_PlayerHeight + m_PlayerSpriteRenderer.bounds.size.y / transform.localScale.y)/8);

        //Time to place the feet collider  according to  player's collider
        m_PlayerFeetCollider.offset = m_PlayerBoxCollider.offset - new Vector2(0f, m_PlayerBoxCollider.bounds.extents.y * 1f / transform.localScale.y);
        m_PlayerFeetCollider.size = new Vector2(m_PlayerBoxCollider.size.x, m_PlayerFeetCollider.size.y);


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        int aux;
        GameObject gaObj = col.gameObject;

        if (gaObj.tag == "Object")
        {
            //to handle object collisions
            Object obj = gaObj.GetComponent<Object>();

            //OBJECT COLLISION HANDLING
            if (obj != null)
            {
                aux = obj.GetPower();

                switch (aux)
                {
                    case GlobalDataScript.NO_POWER:
                        break;


                    //here will bw the code necessary to unlock the different powers
                    case GlobalDataScript.ICE_POWER:
                        break;

                    case GlobalDataScript.FIRE_POWER:
                        break;

                    case GlobalDataScript.STICKY_POWER:
                        break;

                    case GlobalDataScript.POWER_4:
                        break;
                }
                if (obj.GetComponent<SpriteRenderer>().enabled == true)
                {
                    GlobalDataScript.ObjectsPicked++;
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                }
               
                obj.Play();
                Destroy(gaObj, 0.5f);
                
                return;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            GlobalDataScript.IncreasePuntuation(GlobalDataScript.DEATH_POINTS);
            gameObject.SetActive(false);
            Invoke("SetGameOver", 0.5f);

        }
    }

    void SetGameOver()
    {
        GlobalDataScript.LAST_SCENE = SceneManager.GetActiveScene().name;
        GlobalDataScript.TotalDeaths += 1;
        SceneManager.LoadScene("Game Over");
    }


}
