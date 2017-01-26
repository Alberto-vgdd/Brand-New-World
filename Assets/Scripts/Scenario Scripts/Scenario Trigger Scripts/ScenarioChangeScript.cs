using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenarioChangeScript : MonoBehaviour
{
    public string m_ScenarioName;
    public int m_RoomEntranceNumber;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GlobalDataScript.LAST_SCENE = m_ScenarioName;
            GlobalDataScript.ROOM_ENTRANCE = m_RoomEntranceNumber;
            SceneManager.LoadScene(GlobalDataScript.LAST_SCENE); 
        }
    }
}
