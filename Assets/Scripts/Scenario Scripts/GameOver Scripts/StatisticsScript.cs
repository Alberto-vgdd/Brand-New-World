using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsScript : MonoBehaviour {

    public Text m_Statistics;
    public int m_Points;


	// Use this for initialization
	void Start ()
    {
        m_Statistics = GetComponent<Text>();
	}
	
	void Awake ()
    {
        m_Points = (GlobalDataScript.EnemiesKilled * 150 + GlobalDataScript.ObjectsPicked * 300 - GlobalDataScript.TotalDeaths * 1000);
        if (m_Points < 0)
        {
            m_Points = 0;
        }
         
            
            m_Statistics.text =
            "ENEMIES KILLED\t\t\t\t\t" + GlobalDataScript.EnemiesKilled + "\n" +
            "NUMBER OF JUMPS\t\t\t\t" + GlobalDataScript.TotalJumps + "\n" +
            "OBSTACLES DESTROYED\t" + GlobalDataScript.ObstaclesDestroyed + "\n" +
            "DEATHS\t\t\t\t\t\t\t\t\t" + GlobalDataScript.TotalDeaths + "\n" +
            "OBJECTS FOUND\t\t\t\t\t" + GlobalDataScript.ObjectsPicked + "\n\n" +
            "PUNTUACIÓN\t\t\t\t\t" + m_Points;






    }
}
