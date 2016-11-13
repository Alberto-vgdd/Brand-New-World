using UnityEngine;
using System.Collections;

public class FireballScript: MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Platform"))
        {
            Destroy(gameObject, 0.0f);
        }

    }
}
