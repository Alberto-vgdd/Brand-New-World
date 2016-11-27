using UnityEngine;
using System.Collections;

public class StalactiteScript : MonoBehaviour {

    private const float VELOCITY = 0.1f;

    public float dropdown;
    public bool falls;

    private float originalY;
    private bool falling;

	void Start () {
        originalY = this.gameObject.transform.position.y;
        if (!falls)
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (falling)
        {
            this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - VELOCITY, this.transform.position.z);
        }

        if (this.gameObject.transform.position.y < originalY - dropdown)
            Destroy(this);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fireball")
        {
            falling = true;
        }
    }
}
