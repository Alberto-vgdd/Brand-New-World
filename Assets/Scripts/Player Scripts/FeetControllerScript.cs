using UnityEngine;
using System.Collections;

public class FeetControllerScript : MonoBehaviour {
    private bool m_OnGround;
    // Use this for initialization
    void Start ()
    {
        m_OnGround = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D collider)
    {
        //If player hits the platform's ground, he would be able to jump again, not like Harambe. Fuck Isis.
        //"m_PlayerRigidbody.velocity.y <= 1" is due to a bug that causes the player fly when jumping next to an edge
        if (collider.transform.CompareTag("Ground"))
        {
            m_OnGround = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        //If player hits the platform's ground, he would be able to jump again, not like Harambe. Fuck Isis.
        //Here is a bug  where the player cant get m_OnFloor = True, A fix could be using another collider as a trigger.
        if (collider.transform.CompareTag("Ground"))
        {
            m_OnGround = false;
        }
    }

    public bool GetOnGround() { return m_OnGround; }

}
