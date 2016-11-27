using UnityEngine;
using System.Collections;

public class InvisibleWall : MonoBehaviour {

    private SpriteRenderer[] sprites;
    private bool fading, faded;
    private AudioSource audio;
    private AudioClip clip;


	// Use this for initialization
	void Start () {
        sprites = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        faded = fading = false;
        audio = this.gameObject.GetComponent<AudioSource>();
        clip = audio.clip;
	}
	
	// Update is called once per frame
	void Update () {

        if (fading && !faded)
        {
            foreach (SpriteRenderer sr in sprites)
            {
                sr.material.color = new Vector4(sr.material.color.r, sr.material.color.b, sr.material.color.b, 0f);
            }
            faded = true;
        }

        if (!fading && faded)
        {
            foreach (SpriteRenderer sr in sprites)
            {
                sr.material.color = new Vector4(sr.material.color.r, sr.material.color.b, sr.material.color.b, 1f);
            }
            faded = false;
        }
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            audio.PlayOneShot(clip);
            fading = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        audio.PlayOneShot(clip);
        if (col.tag == "Player")
        {
            audio.PlayOneShot(clip);
            fading = false;
        }
    }
}
