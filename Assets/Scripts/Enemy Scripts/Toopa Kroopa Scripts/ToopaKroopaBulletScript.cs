using UnityEngine;
using System.Collections;

public class ToopaKroopaBulletScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 1.9f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Platform"))
        {
            Destroy(gameObject, 0.0f);
        }

    }
}
