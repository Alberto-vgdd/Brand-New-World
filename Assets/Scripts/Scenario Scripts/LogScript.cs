using UnityEngine;
using System.Collections;

public class LogScript : MonoBehaviour {

    void OnCollisionEnter2D(Collider2D col)
    {
        if (col.tag == "Fireball")
            Destroy(this.gameObject);
    }
}
