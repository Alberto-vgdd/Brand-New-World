using UnityEngine;
using System.Collections;

public class LogScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Fireball")
            Destroy(this.gameObject);
    }
}
