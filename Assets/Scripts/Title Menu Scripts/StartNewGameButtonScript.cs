using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartNewGameButtonScript : MonoBehaviour
{
    private Button m_StartNewGameButton;
    public Sprite[] m_ButtonSprites;


    void Start()
    {
        m_StartNewGameButton = GetComponent<Button>();
    }

     public void ButtonHover()
    {
        m_StartNewGameButton.image.overrideSprite = m_ButtonSprites[1];
    }

    public void ButtonNotHover()
    {
        m_StartNewGameButton.image.overrideSprite = m_ButtonSprites[0];
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("L01");
    }
   
}
