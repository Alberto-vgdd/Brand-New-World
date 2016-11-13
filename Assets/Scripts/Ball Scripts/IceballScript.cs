using UnityEngine;
using System.Collections;

public class IceballScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject, 0.0f);
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            Destroy(gameObject, 0.0f);
        }
    }
}
