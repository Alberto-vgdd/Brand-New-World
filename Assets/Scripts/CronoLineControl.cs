using UnityEngine;
using System.Collections;

public class CronoLineControl : MonoBehaviour {

    private const float MOV_SPEED = 8f;
    private RectTransform mainPanel;
    private float originalRight;
    private GameObject pauseMenu, cronoLine;
    private CanvasControllerScript cControl;
    private float openTime; //time it has been opened
    public Vector3 pos;
    public float right;
    public bool prueba;

	// Use this for initialization
	void Start () {
        mainPanel = GameObject.Find("CronoLine").GetComponent<RectTransform>(); ;
        originalRight = mainPanel.localPosition.x;
        right = originalRight;
        cronoLine = this.gameObject;
        pauseMenu = GameObject.Find("PauseMenu");
        cControl = GameObject.Find("CanvasController").GetComponent<CanvasControllerScript>();
	}


    void OnEnable()
    {
        openTime = 0f;
    }
	// Update is called once per frame
	void Update () {

        openTime += 0.02f;   //1 second at 50fps

         if(Input.GetKey("a"))
         {

            pos = mainPanel.localPosition;
             //there is no displacement unlike the other case because we don't want it to get any further to the left than the original position
            if(mainPanel.localPosition.x <= originalRight)
                mainPanel.localPosition = new Vector3(mainPanel.localPosition.x + MOV_SPEED,
                                                        mainPanel.localPosition.y,
                                                        mainPanel.localPosition.z);
         }

         if (Input.GetKey("d"))
         {
             pos = mainPanel.localPosition;
             //we add a displacement, because it has to move half of the sprite width to right
             if (mainPanel.localPosition.x >= -originalRight - 1080)
             mainPanel.localPosition = new Vector3(mainPanel.localPosition.x - MOV_SPEED,
                                                         mainPanel.localPosition.y,
                                                         mainPanel.localPosition.z);
         }

         if (Input.GetKey("escape"))
         {
             //to avoid closing it when its opened after picking an object
             if (openTime >= 1)
             {
                 mainPanel.localPosition = new Vector3(originalRight,
                                                            mainPanel.localPosition.y,
                                                            mainPanel.localPosition.z);
                 GlobalDataScript.CRONOLINE = false;
                 cControl.ResumeGame();
                 this.gameObject.SetActive(false);
             }
         }
            
	
	}
}
