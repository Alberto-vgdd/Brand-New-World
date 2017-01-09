using UnityEngine;
using System.Collections;

public class LogScript : MonoBehaviour {

    private BoxCollider2D col;
    private SpriteRenderer spriteRend;
    private AudioSource audio;
    private AudioClip clip;

    void Start()
    {
        col = this.gameObject.GetComponent<BoxCollider2D>();
        spriteRend = this.gameObject.GetComponent<SpriteRenderer>();
        audio = this.gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == GlobalDataScript.FIREBALL_TAG)
        {
            audio.PlayOneShot(clip);
            col.collider.enabled= false;
            spriteRend.enabled = false;
            GlobalDataScript.ObstaclesDestroyed = GlobalDataScript.ObstaclesDestroyed + 1;
            Destroy(this.gameObject, 0.2f);
        }
    }
}
