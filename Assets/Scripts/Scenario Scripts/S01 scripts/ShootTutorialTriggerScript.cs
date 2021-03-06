﻿using UnityEngine;
using System.Collections;

public class ShootTutorialTriggerScript : MonoBehaviour {

    public Transform m_CanvasController;
    public Sprite m_MessageImage;

    public Transform m_CameraController;
    public Transform m_Worm;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            LookAtBothPlayerAndEnemy();
            m_CanvasController.GetComponent<CanvasControllerScript>().SendMessage(m_MessageImage, 6);
            Invoke("LookAtPlayer", 4f);

            Destroy(this.gameObject, 5f);
        }
    }

    void LookAtPlayer()
    {
        m_CameraController.GetComponent<CameraControllerScript>().m_Targets[1] = null;
    }

    void LookAtBothPlayerAndEnemy()
    {
        m_CameraController.GetComponent<CameraControllerScript>().m_Targets[1] = m_Worm;
    }
}
