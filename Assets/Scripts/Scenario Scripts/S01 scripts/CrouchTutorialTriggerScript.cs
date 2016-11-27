using UnityEngine;
using System.Collections;

public class CrouchTutorialTriggerScript : MonoBehaviour
{
    public  Transform m_CanvasController;
    public  Sprite m_MessageImage;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
           
            m_CanvasController.GetComponent<CanvasControllerScript>().SendMessage(m_MessageImage, 5);
            DestroyObject(gameObject);
        }
    }
}
