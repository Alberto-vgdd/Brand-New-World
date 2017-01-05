using UnityEngine;
using System.Collections;

public class CrouchZoneTriggerScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<PlayerMovementScript>().LockCrouch();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<PlayerMovementScript>().UnlockCrouch();
        }
    }
}
