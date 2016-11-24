using UnityEngine;
using System.Collections;

public class PlayerShootScript : MonoBehaviour {

    //We need these variables to "import" and instantiate bullets
    public  Rigidbody2D m_BulletPrefab;
    private Rigidbody2D m_BulletClone;
    public float m_HandForce;


    //We need this transform to update hand's position & rotation
    private Transform m_HandTransform;
    private Vector3 m_HandPosition;
    private Quaternion m_HandRotation;

    //We check where is the mouse
    private Vector3 m_CrosshairPosition;

    //...and which direcction should the bullet go.
    private Vector2 m_BulletDirection;


    //The texture we are going to use as crosshair.
    public Texture2D m_MouseCrosshairTexture;


    //Variables to flip the character
    private bool m_FacingRight;
    private Animator m_PlayerAnimator;


	// Use this for initialization
	void Start ()
    {
        ApplyCursorTexture();
        InitializeVariables();
    }
    void ApplyCursorTexture()
    {
        //Cursor.SetCursor(m_MouseCrosshairTexture,  Vector2.zero, CursorMode.Auto);
        Cursor.SetCursor(m_MouseCrosshairTexture, new Vector2(m_MouseCrosshairTexture.width / 2, m_MouseCrosshairTexture.height / 2), CursorMode.Auto);
    }
    void InitializeVariables()
    {
        m_HandTransform = transform.FindChild("Hand");
        m_PlayerAnimator = GetComponent<Animator>();
        m_HandForce = 5f;
        m_FacingRight = true;
    }



	// Update is called once per frame
    void Update()
    {
        //The player won't shoot if the game is paused or  the context menu is in the screen. The crosshair won't be updated either.
        if (!GlobalDataScript.PAUSE_MENU && !GlobalDataScript.CONTEXT_MENU && GlobalDataScript.INPUT_ENABLED)
        {
            UpdateCrosshairPosition();

            if (Input.GetButtonDown("Shoot"))
            {
                    CalculateBulletTrayectory();
                    InstantiateBullet();
            }
        }

    }



    void UpdateCrosshairPosition()
    {
        //Now we look for the mouse position
        m_CrosshairPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }
    void CalculateBulletTrayectory()
    {
        //First we update hand's values
        m_HandPosition = m_HandTransform.position;
        m_HandRotation = m_HandTransform.rotation;

        //We calculate a normalized vector for  the Addforce function of the bullet.
        m_BulletDirection = new Vector2(m_CrosshairPosition.x - m_HandPosition.x, m_CrosshairPosition.y - m_HandPosition.y).normalized;
    }

    void InstantiateBullet()
    {
        m_BulletClone = Instantiate(m_BulletPrefab, m_HandPosition, m_HandRotation) as Rigidbody2D;
        m_BulletClone.AddForce(m_BulletDirection * m_HandForce);
    }


    void FixedUpdate()
    {
            if (m_FacingRight && transform.position.x > m_CrosshairPosition.x || !m_FacingRight && transform.position.x < m_CrosshairPosition.x)
            {
                FlipCharacter();
            }
    }
    void FlipCharacter()
    {
        m_FacingRight = !m_FacingRight;
        m_HandTransform.localPosition = new Vector3(-m_HandTransform.localPosition.x, m_HandTransform.localPosition.y, m_HandTransform.localPosition.z);
        
    }



    public bool GetFacing()
    {
        return m_FacingRight;
    }



}
