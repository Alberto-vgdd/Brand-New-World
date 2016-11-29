using UnityEngine;
using System.Collections;

public class StalactiteScript : MonoBehaviour {

    private const float VELOCITY = 0.1f;

    public float dropdown;
    public bool falls;


    private AudioSource audio;
    private AudioClip clip;

    private float actualVelocity;
    private float originalY;
    private bool falling;

	void Start () {

        audio = this.gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
        originalY = this.gameObject.transform.position.y;
        if (!falls)
        {
            Destroy(this);
        }

        actualVelocity = 0.1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (falling)
        {
            this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - actualVelocity, this.transform.position.z);
            if (actualVelocity < VELOCITY)
                actualVelocity += 0.1f;
        }

        if (this.gameObject.transform.position.y < originalY - dropdown)
            Destroy(this);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Fireball")
        {
            falling = true;
            audio.PlayOneShot(clip);
        }
    }
}
