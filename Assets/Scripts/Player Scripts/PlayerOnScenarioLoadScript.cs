using UnityEngine;
using System.Collections;

public class PlayerOnScenarioLoadScript : MonoBehaviour
{
    private Vector3 m_RoomEntrance0 = new Vector3(-7.955f, 2.564f, 0f);
    private Vector3 m_RoomEntrance1 = new Vector3(6.986f, -4.175f, 0f);

    // Use this for initialization
    void Awake ()
    {
        switch (GlobalDataScript.ROOM_ENTRANCE)
        {
            case 0:
                transform.position = m_RoomEntrance0;
                break;
            case 1:
                transform.position = m_RoomEntrance1;
                break;
        }

    }
}
