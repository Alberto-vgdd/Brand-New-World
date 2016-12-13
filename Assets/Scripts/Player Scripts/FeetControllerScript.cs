using UnityEngine;
using System.Collections;

public class FeetControllerScript : MonoBehaviour {
    public  bool m_OnGround;
    public bool m_On2Grounds;
    // Use this for initialization
    void Start ()
    {
        m_OnGround = false;
        m_On2Grounds = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void OnTriggerEnter2D(Collider2D collider)
    {
        //If player hits the platform's ground, he would be able to jump again, not like Harambe. Fuck Isis.
        //"m_PlayerRigidbody.velocity.y <= 1" is due to a bug that causes the player fly when jumping next to an edge
        if (collider.transform.CompareTag("Ground"))
        {
            if (m_OnGround)
            {
                m_On2Grounds = true;
            }
            else
            {
                m_On2Grounds = false;
                m_OnGround = true;
            }

            
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        //If player hits the platform's ground, he would be able to jump again, not like Harambe. Fuck Isis.
        //Here is a bug  where the player cant get m_OnFloor = True, A fix could be using another collider as a trigger.
        if (collider.transform.CompareTag("Ground"))
        {
            if (!m_On2Grounds)
            {
                m_OnGround = false;
            }
            else
            {
                m_On2Grounds = false;
            }
            
        }
    }

    public bool GetOnGround() { return m_OnGround; }

}
