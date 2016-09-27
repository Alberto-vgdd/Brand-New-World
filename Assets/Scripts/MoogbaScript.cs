using UnityEngine;
using System.Collections;

public class MoogbaScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
   {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Fireball"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
