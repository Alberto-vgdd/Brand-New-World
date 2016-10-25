using UnityEngine;
using System.Collections;

public class CanvasControllerScript: MonoBehaviour {

    private GameObject m_Canvas;
    private GameObject m_ContextMenu;
    private RectTransform m_ContextMenuTransform;
    private bool m_OpenContextMenu;
    private bool m_PauseGame;
 

    //GlobalData should store Time.fixedDeltaTime for every different state to avoid bugs.
    //A fix could be don't let the player pause the game with context menu enabled.



    void Start ()
    {
        InitializeVariables();
        PlaceRectTransform();
        HideContextMenu();
    }

    void Update()
    {
        //Check if the Context Menu Should be Displayed
        if(Input.GetButton("BallsMenu") == true) { m_OpenContextMenu = true; } else { m_OpenContextMenu = false; }
    }

    void FixedUpdate()
    {
        if (m_OpenContextMenu && !GlobalDataScript.CONTEXT_MENU)
        {
            ShowContextMenu();
        }
        else if (!m_OpenContextMenu && GlobalDataScript.CONTEXT_MENU)
        {
            HideContextMenu();
        }
    }

    void  InitializeVariables()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_ContextMenu = GameObject.Find("ContextMenu");
        m_ContextMenuTransform = m_ContextMenu.GetComponent<RectTransform>();

        m_OpenContextMenu = false;
        m_PauseGame = false;

        //Center Context Menu.
        m_ContextMenuTransform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
    }

    public void HideContextMenu()
    {
        m_ContextMenu.SetActive(false);
        GlobalDataScript.CONTEXT_MENU = false;

        //Properly set TimeScale back to its default value.
        Time.timeScale = GlobalDataScript.NORMAL_TIME_SPEED;
        Time.fixedDeltaTime =GlobalDataScript.NORMAL_FIXED_DELTA_TIME;

    }


    public void ShowContextMenu()
    {
       m_ContextMenu.SetActive(true);
       GlobalDataScript.CONTEXT_MENU = true;

        //Properly set TimeScale to slow motion.
        Time.timeScale = GlobalDataScript.SLOW_TIME_SPEED;
        Time.fixedDeltaTime = GlobalDataScript.SLOW_FIXED_DELTA_TIME;
    }
    
    void PauseGame()
    {
        GlobalDataScript.PAUSE_MENU = true;
        Time.timeScale = GlobalDataScript.PAUSED_TIME_SPEED;
        Time.fixedDeltaTime = GlobalDataScript.PAUSED_FIXED_DELTA_TIME;
    }

    void ResumeGame()
    {
        GlobalDataScript.PAUSE_MENU = false;
        Time.timeScale = GlobalDataScript.NORMAL_TIME_SPEED;
        Time.fixedDeltaTime = GlobalDataScript.NORMAL_FIXED_DELTA_TIME;
    }

    void PlaceRectTransform()
    {
        for (int i = 1; i <= 8; i++)
        {
            m_ContextMenuTransform.FindChild(""+i).GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(150f * Mathf.Sin(Mathf.Deg2Rad * 45f*i), 150f * Mathf.Cos(Mathf.Deg2Rad * 45f * i));
        }
    }
}
