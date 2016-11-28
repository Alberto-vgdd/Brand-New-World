using UnityEngine;
using System.Collections;

public class CronoLineControl : MonoBehaviour {

    private const float MOV_SPEED = 4f;
    private RectTransform mainPanel;
    private float originalRight;
    private GameObject pauseMenu, cronoLine;
    private CanvasControllerScript cControl;

    public bool prueba;
    public Vector3 pos;
    public float right;

	// Use this for initialization
	void Start () {
        mainPanel = GameObject.Find("CronoLine_mainPanel").GetComponent<RectTransform>(); ;
        originalRight = mainPanel.localPosition.x;
        cronoLine = this.gameObject;
        pauseMenu = GameObject.Find("PauseMenu");
        cControl = GameObject.Find("CanvasController").GetComponent<CanvasControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {

        /*if (Mathf.Abs(Input.GetAxis("MovementAxisX")) > 0.1)
        {
           
            if (Input.GetAxis("MovementAxisX") > 0) //right
            {*/
               
         if(Input.GetKey("d"))
         {
            prueba = true;

            pos = mainPanel.localPosition;
            if(mainPanel.localPosition.x >= -originalRight)
                mainPanel.localPosition = new Vector3(mainPanel.localPosition.x - MOV_SPEED,
                                                        mainPanel.localPosition.y,
                                                        mainPanel.localPosition.z);
         }

         if (Input.GetKey("a"))
         {
             prueba = true;
             if (mainPanel.localPosition.x <= originalRight)
             mainPanel.localPosition = new Vector3(mainPanel.localPosition.x + MOV_SPEED,
                                                         mainPanel.localPosition.y,
                                                         mainPanel.localPosition.z);
         }

         if (Input.GetKey("escape"))
         {
             this.gameObject.SetActive(false);
         }
            
	
	}
}
