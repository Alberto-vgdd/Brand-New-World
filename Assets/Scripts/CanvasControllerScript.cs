using UnityEngine;
using System.Collections;

public class CanvasControllerScript: MonoBehaviour {

    private GameObject m_Canvas;
    private GameObject m_ContextMenu;
    private RectTransform m_ContextMenuTransform;
    private bool m_PauseGame;
    private float centerX, centerY;
    private float clickX, clickY;
    public bool prueba;
 

    //GlobalData should store Time.fixedDeltaTime for every different state to avoid bugs.
    //A fix could be don't let the player pause the game with context menu enabled.



    void Start ()
    {
        InitializeVariables();
        PlaceRectTransform();
        HideContextMenu();
        setCenter();
    }

    private void setCenter()
    {
        
    }

    void Update()
    {
        if (!GlobalDataScript.PAUSE_MENU)
        {
            //Check if the Context Menu Should be Displayed
            if (Input.GetButtonDown("BallsMenu") == true)
                ShowContextMenu();

            else if (Input.GetButtonUp("BallsMenu"))
                HideContextMenu();
        }

        if (Input.GetButtonDown("Pause"))
        {

            if (!GlobalDataScript.PAUSE_MENU)
                PauseGame();

            else if (GlobalDataScript.PAUSE_MENU)
                ResumeGame();
        }

        if (Input.GetButtonUp("BallsMenu"))
        {
            clickX = Input.mousePosition.x;
            clickY = Input.mousePosition.y;
            determineSelection();
        }
    }

    private void determineSelection()
    {
        float centerLineLength;
        float centerToClickLength;
        Vector2 centerToClick;

        //2nd and 4th quadrant
        if ((clickX > centerX && clickY < centerY) || (clickX < centerX && clickY > centerY))
            centerLineLength = Mathf.Abs(clickX - centerX);
        //1st and 3rd quadrant
        else
            centerLineLength = Mathf.Abs(clickY - centerY);

        centerToClick = new Vector2(clickX - centerX, clickY - centerY);
        centerToClickLength = centerToClick.magnitude;
    }



    void  InitializeVariables()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_ContextMenu = GameObject.Find("ContextMenu");
        m_ContextMenuTransform = m_ContextMenu.GetComponent<RectTransform>();
        m_PauseGame = false;

        //Center Context Menu.
        centerX = Screen.width / 2f;
        centerY = Screen.height / 2f;
        m_ContextMenuTransform.position = new Vector3(centerX, centerY, 0);
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

        if (GlobalDataScript.CONTEXT_MENU)
        {
            if (!Input.GetButton("BallsMenu"))
            {
                HideContextMenu();
                Time.timeScale = GlobalDataScript.NORMAL_TIME_SPEED;
                Time.fixedDeltaTime = GlobalDataScript.NORMAL_FIXED_DELTA_TIME;
            }
            else
            {
                Time.timeScale = GlobalDataScript.SLOW_TIME_SPEED;
                Time.fixedDeltaTime = GlobalDataScript.SLOW_FIXED_DELTA_TIME;
            }
        }
        else
        {
            Time.timeScale = GlobalDataScript.NORMAL_TIME_SPEED;
            Time.fixedDeltaTime = GlobalDataScript.NORMAL_FIXED_DELTA_TIME;
        }

        if (GlobalDataScript.CROUCHING)
        {
            if (!Input.GetButton("Crouch"))
                GlobalDataScript.CROUCHING = false;
        }
    }

    void PlaceRectTransform()
    {
        for (int i = 1; i <= 8; i++)
        {
            m_ContextMenuTransform.FindChild(""+i).GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(150f * Mathf.Sin(Mathf.Deg2Rad * 45f*i), 150f * Mathf.Cos(Mathf.Deg2Rad * 45f * i));
        }
    }

    public void NewFragment(string fragment, int date)
    {
        //aqui se alamcenara el fragmento recogido para mostrarlo en la crono-linea
        prueba = !prueba;
    }
}
