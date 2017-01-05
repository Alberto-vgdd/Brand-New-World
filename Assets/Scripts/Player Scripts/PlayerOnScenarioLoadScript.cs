using UnityEngine;
using System.Collections;

public class PlayerOnScenarioLoadScript : MonoBehaviour
{
    private Vector3 m_RoomEntrance0 = new Vector3(-7.955f, 2.564f, 0f);
    private Vector3 m_RoomEntrance1 = new Vector3(5.535f, -4.175f, 0f);
    private Vector3 m_RoomEntrance2 = new Vector3(-8.986f, 0.0f, 0f);
    private Vector3 m_RoomEntrance3 = new Vector3(6.234f, 0.863f, 0f);
    private Vector3 m_RoomEntrance4 = new Vector3(-0.79f, -5.32f, 0f);
    private Vector3 m_RoomEntrance5 = new Vector3(8.269f, 2.729f, 0f);
    private Vector3 m_RoomEntrance6 = new Vector3(-5.993f, 1.011f, 0f);
    private Vector3 m_RoomEntrance7 = new Vector3(12.984f, 1.17f, 0f);
    private Vector3 m_RoomEntrance99 = new Vector3(-9.78f, 6.74f, 0f);

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
            case 2:
                transform.position = m_RoomEntrance2;
                break;
            case 3:
                transform.position = m_RoomEntrance3;
                break;
            case 4:
                transform.position = m_RoomEntrance4;
                break;
            case 5:
                transform.position = m_RoomEntrance5;
                break;
            case 6:
                transform.position = m_RoomEntrance6;
                break;
            case 7:
                transform.position = m_RoomEntrance7;
                break;
            case 99:
                transform.position = m_RoomEntrance99;
                break;
        }

    }
}
