using UnityEngine;
using System.Collections;

public class CrouchTutorialTriggerScript : MonoBehaviour
{
    public  Transform m_CanvasController;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
           
            m_CanvasController.GetComponent<CanvasControllerScript>().ShowMessage(1);
            DestroyObject(gameObject);
        }
    }
}
