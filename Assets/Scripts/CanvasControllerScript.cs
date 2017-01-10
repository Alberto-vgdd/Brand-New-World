using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CanvasControllerScript : MonoBehaviour
{

    private const float TEMPLATE_WIDTH = 270f;
    private const float TEMPLATE_HEIGHT = 300f;
    private const float OCUPPIED = 0;
    private const float NOT_OCCUPIED = -1;
    private const float OBJECT_PICKED_PANEL_TIME = 3f;
    private const int MAXIMMUM_FRAGMENTS = 20;  //number of objects that can be picked up

    //CANVAS ELEMENTS
    private GameObject m_Canvas;
    private GameObject m_ContextMenu;
    private RectTransform m_ContextMenuTransform, a;
    private GameObject pauseMenu;
    private GameObject cronoLine;
    private GameObject cronoLineFragments;
    private GameObject objectPickedPanel;
    private Image objectPickedPanelImage;

    //CRONO LINE template
    private RectTransform CLtemplateUP;
    private Text templateDateUP;
    private Text templateFragmentUP;

    private PlayerShootScript playerShoot;
    private bool m_PauseGame;
    private float centerX, centerY;
    private float clickX, clickY;

    //CRONO LINE AND ITS ELEMENTS MEASURES
    private float cronoLineLength, cronoLineHeight;
    private float templateWidth, templateHeight;
    //LIST THAT STORES EVERY FRAGMENTS X POSITION AND IF THE UPPER AND LOWER SPACES HAVE BEEN OCUPIED
     private float[,] templatesSet = new float[MAXIMMUM_FRAGMENTS, 3];   //[position of the fragment, up occuped, down occuped]

    
    //time remaining for the object picked pannel to disappear
     private float objectPickedPanelRemainingTime;
    //bool to keep substracting units to the time and to make it disappear
     private bool countdown, vanishing;



    public Rigidbody2D[] balls = new Rigidbody2D[8];


    private Image m_Message;

    //GlobalData should store Time.fixedDeltaTime for every different state to avoid bugs.
    //A fix could be don't let the player pause the game with context menu enabled.



    public void Start()
    {
        initializeVariables();
        placeRectTransform();
        hideContextMenu();
        reloadCronoLine();
    }



    public void Update()
    {
        if (countdown)
        {
            objectPickedPanelRemainingTime -= Time.deltaTime;
            if (objectPickedPanelRemainingTime <= 0)
            {
                countdown = false;
                vanishing = true;
            }
        }

        //to vanish the sign before making disappear both it and its effects
        if (vanishing)
        {
            objectPickedPanelImage.color = new Vector4(objectPickedPanelImage.color.r, objectPickedPanelImage.color.g,
                objectPickedPanelImage.color.b, objectPickedPanelImage.color.a - Time.deltaTime);
            if (objectPickedPanelImage.color.a <= 0)
            {
                vanishing = false;
                //reset the alpha component
                objectPickedPanelImage.color = new Vector4(objectPickedPanelImage.color.r, objectPickedPanelImage.color.g,
                objectPickedPanelImage.color.b, 0.8f);

                objectPickedPanel.SetActive(false);
                GlobalDataScript.OBJECT_PICKED_PANEL_ACTIVE = false;
            }
        }

        if (GlobalDataScript.INPUT_ENABLED)
        {
            if (!GlobalDataScript.PAUSE_MENU)
            {
                //Check if the Context Menu Should be Displayed
                if (Input.GetButtonDown("BallsMenu") == true)
                    showContextMenu();

                else if (Input.GetButtonUp("BallsMenu"))
                    hideContextMenu();
            }

            if (Input.GetButtonDown("Pause"))
            {
                if (!GlobalDataScript.CRONOLINE)
                {
                    if (!GlobalDataScript.PAUSE_MENU)
                    {
                        PauseGame();

                        //if the game is paused while the sign is visible, the cronoline is opened
                        //and the sign disappears
                        if (GlobalDataScript.OBJECT_PICKED_PANEL_ACTIVE)
                        {
                            countdown = false;
                            vanishing = false;
                            objectPickedPanelImage.color = new Vector4(objectPickedPanelImage.color.r, objectPickedPanelImage.color.g,
                            objectPickedPanelImage.color.b, 0.8f);

                            objectPickedPanel.SetActive(false);
                            GlobalDataScript.OBJECT_PICKED_PANEL_ACTIVE = false;
                            OnCronoLineButton();
                        }
                    }

                    else if (GlobalDataScript.PAUSE_MENU)
                        ResumeGame();
                }
            }

            if (Input.GetButtonUp("BallsMenu"))
            {
                clickX = Input.mousePosition.x;
                clickY = Input.mousePosition.y;
                determineSelection();
            }
        }
    }

    private void determineSelection()
    {
        float centerLineLength;
        float centerToClickLength;
        float angle;
        int quadrant;
        Vector2 centerToClick;

        //2nd and 4th quadrant
        if ((clickX > centerX && clickY < centerY) || (clickX < centerX && clickY > centerY))
        {
            centerLineLength = Mathf.Abs(clickX - centerX);
            if (clickX > centerX) //2nd
                quadrant = 2;
            else //4th
                quadrant = 4;
        }
        //1st and 3rd quadrant
        else
        {
            centerLineLength = Mathf.Abs(clickY - centerY);
            if (clickX > centerX) //1st
                quadrant = 1;
            else //3rd
                quadrant = 3;
        }

        centerToClick = new Vector2(clickX - centerX, clickY - centerY);
        centerToClickLength = centerToClick.magnitude;
        angle = Mathf.Rad2Deg * Mathf.Acos(centerLineLength / centerToClickLength);

        switch (quadrant) //we add the angle corresponding to the quadrant
        {
            case 1:
                break;
            case 2:
                angle += 90;
                break;
            case 3:
                angle += 180;
                break;
            case 4:
                angle += 270;
                break;
        }


        //depending on the angle we determine which option was selected
        if (angle > 20 && angle <= 60)
            playerShoot.m_BulletPrefab = balls[0];

        else if (angle > 60 && angle <= 100)
            playerShoot.m_BulletPrefab = balls[1];

        else if (angle > 100 && angle <= 140)
            playerShoot.m_BulletPrefab = balls[2];

       /* else if (angle > 140 && angle <= 200)
            playerBall = balls[3];*/

        else
            playerShoot.m_BulletPrefab = balls[0];



    }



    private void initializeVariables()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_ContextMenu = GameObject.Find("ContextMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        cronoLine = GameObject.Find("CronoLine");
        CLtemplateUP = GameObject.Find("CLtemplateUP").GetComponent<RectTransform>();
        templateDateUP = GameObject.Find("CLtemplateUP_date").GetComponent<Text>();
        templateFragmentUP = GameObject.Find("CLtemplateUP_fragment").GetComponent<Text>();
        cronoLineFragments = GameObject.Find("CLfragments");
        cronoLineLength = GameObject.Find("CronoLine_mainPanel").GetComponent<RectTransform>().rect.width * 1.55f;
        cronoLineHeight = GameObject.Find("CronoLine_mainPanel").GetComponent<RectTransform>().rect.height;
        objectPickedPanel = GameObject.Find("PickedObjectSign");
        objectPickedPanelImage = GameObject.Find("PickedObjectSign_Panel").GetComponent<Image>();


        CLtemplateUP.gameObject.SetActive(false);

        m_Message = transform.Find("Canvas").Find("Message").GetComponent<Image>();

        //initializing templates array
        for (int i = 0; i < MAXIMMUM_FRAGMENTS; i++)
        {
            templatesSet[i, 0] = templatesSet[i, 1] = templatesSet[i, 2] = -1f;
        }


        m_ContextMenuTransform = m_ContextMenu.GetComponent<RectTransform>();
        m_PauseGame = false;
        pauseMenu.SetActive(false);
        cronoLine.SetActive(false);
        objectPickedPanel.SetActive(false);



        //Center Context Menu.
        centerX = Screen.width / 2f;
        centerY = Screen.height / 2f;
        m_ContextMenuTransform.position = new Vector3(centerX, centerY, 0);

        playerShoot = GameObject.Find("Player").GetComponent<PlayerShootScript>();
        playerShoot.m_BulletPrefab = balls[0];///initialize to fireball;
    }

    private void hideContextMenu()
    {
        m_ContextMenu.SetActive(false);
        GlobalDataScript.CONTEXT_MENU = false;

        //Properly set TimeScale back to its default value.
        Time.timeScale = GlobalDataScript.NORMAL_TIME_SPEED;
        Time.fixedDeltaTime = GlobalDataScript.NORMAL_FIXED_DELTA_TIME;

    }


    private void showContextMenu()
    {
        m_ContextMenu.SetActive(true);
        GlobalDataScript.CONTEXT_MENU = true;

        //Properly set TimeScale to slow motion.
        Time.timeScale = GlobalDataScript.SLOW_TIME_SPEED;
        Time.fixedDeltaTime = GlobalDataScript.SLOW_FIXED_DELTA_TIME;
    }


    public void PauseGame()
    {
        GlobalDataScript.PAUSE_MENU = true;
        Time.timeScale = GlobalDataScript.PAUSED_TIME_SPEED;
        Time.fixedDeltaTime = GlobalDataScript.PAUSED_FIXED_DELTA_TIME;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        GlobalDataScript.PAUSE_MENU = false;
        pauseMenu.SetActive(false);

        if (GlobalDataScript.CONTEXT_MENU)
        {
            if (!Input.GetButton("BallsMenu"))
            {
                hideContextMenu();
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

    private void placeRectTransform()
    {
        for (int i = 1; i <= 8; i++)
        {
            m_ContextMenuTransform.FindChild("" + i).GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(150f * Mathf.Sin(Mathf.Deg2Rad * 45f * i), 150f * Mathf.Cos(Mathf.Deg2Rad * 45f * i));
        }
    }

    public void NewFragment(string fragment, int date)
    {
        bool upOccupied, downOccupied, ok; //to see if there is free space and to exit the functions bucle when the work has been done correctly
        float position = 0;
        string auxString;
        int iteration = 0;


        upOccupied = downOccupied = ok = false;


        while (!ok)  //we check until we find a free position
        {

            position = (cronoLineLength / (GlobalDataScript.GetDates()[1] - GlobalDataScript.GetDates()[0]) * date) - (TEMPLATE_WIDTH / 2f); //we obtain the corresponding x position of the fragment's left border

            //to avoid placing the object outside the borders, it has to be > 4
            while (position < 4 + TEMPLATE_WIDTH / 2 + 10)
                position++;

            while (position > cronoLineLength - TEMPLATE_WIDTH / 2 - 10)
                position--;



            for (int i = 0; i < MAXIMMUM_FRAGMENTS; i++)
            {
                if (templatesSet[i, 0] == -1)
                    break;

                if ((templatesSet[i, 0] + TEMPLATE_WIDTH > position - 10) && (templatesSet[i, 0] < position + TEMPLATE_WIDTH + 10)) //the position is occupied
                {

                    if (templatesSet[i, 1] == OCUPPIED) //which position is not available?
                        upOccupied = true;
                    if (templatesSet[i, 2] == OCUPPIED)
                        downOccupied = true;
                }
            }

            //if both are occupied, we need to set the date again 
            if (upOccupied && downOccupied)
            {
                date = UnityEngine.Random.Range(GlobalDataScript.GetDates()[0] + 10, GlobalDataScript.GetDates()[1] - 9);
                upOccupied = downOccupied = false;
                continue;
            }
            else
                ok = true;

        }


        //we update the array with the new element
        for (int i = 0; i < MAXIMMUM_FRAGMENTS; i++)
        {
            if (templatesSet[i, 0] == -1)
            {
                templatesSet[i, 0] = position;

                //up is occupied always (the fragment lays there or there was another there before), but down will only be occupied if up was occupied by another fragment
                templatesSet[i, 1] = OCUPPIED;
                if (upOccupied)
                    templatesSet[i, 2] = OCUPPIED;
                else
                    templatesSet[i, 2] = NOT_OCCUPIED;

                break;
            }

        }
        GameObject aux;

        templateDateUP.text = Convert.ToString(date);
        templateFragmentUP.text = fragment;

        aux = GameObject.Instantiate(CLtemplateUP.gameObject);

        aux.transform.SetParent(cronoLineFragments.transform);
        aux.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);



        if (upOccupied)
        {
            aux.GetComponent<RectTransform>().anchoredPosition = new Vector2(position, -cronoLineHeight / 2 - 230f);
        }
        else
        {
            aux.GetComponent<RectTransform>().anchoredPosition = new Vector2(position, cronoLineHeight / 2 - 200f);
        }

        aux.SetActive(true);


        showObjectPickedPanel();

    }

    private void reloadCronoLine()
    {
        int iterator = 0;
        string aux = "algo";

        while (true)
        {
            if (iterator >= MAXIMMUM_FRAGMENTS)
                break;
            aux = GlobalDataScript.PickedFragments[iterator][0];
            if (aux == null)
                break;
            NewFragment(GlobalDataScript.PickedFragments[iterator][1], Convert.ToInt32(GlobalDataScript.PickedFragments[iterator][0]));
            iterator++;
        }

    }



    private void showObjectPickedPanel()
    {
        objectPickedPanel.SetActive(true);
        objectPickedPanelRemainingTime = OBJECT_PICKED_PANEL_TIME;
        countdown = true;
        GlobalDataScript.OBJECT_PICKED_PANEL_ACTIVE = true;
    }

    //MENU BUTTON'S FUNCTIONS

    public void OnResumeButton()
    {
        ResumeGame();
    }

    public void OnCronoLineButton()
    {
        pauseMenu.SetActive(false);
        cronoLine.SetActive(true);
        GlobalDataScript.CRONOLINE = true;
    }

    public void OnExitButton()
    {
        //optional save code
        Application.Quit();
    }



    public void SendMessage(Sprite NewImage, int TimeOnScreen)
    {
        //Update the image that is going to be shown.
        m_Message.overrideSprite = NewImage;

        //Pause the game, and then display the tutorial text
        //IMPORTANT, TIME.FIXEDDELTATIME HAS TO  BE CHANGED TO 0.02 
        ShowMessage();

        //Wait for 7 seconds, and then resume the game.
        Invoke("HideMessage", TimeOnScreen);
    }


    void ShowMessage()
    {
        GlobalDataScript.INPUT_ENABLED = false;
        m_Message.gameObject.SetActive(true);
    }

    void HideMessage()
    {
        m_Message.gameObject.SetActive(false);
        GlobalDataScript.INPUT_ENABLED = true;
    }

}