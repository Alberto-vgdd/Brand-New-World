using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    private Button m_ExitButton;
    public Sprite[] m_ButtonSprites;


    void Start()
    {
        m_ExitButton = GetComponent<Button>();
    }

    public void ButtonHover()
    {
        m_ExitButton.image.overrideSprite = m_ButtonSprites[1];
    }

    public void ButtonNotHover()
    {
        m_ExitButton.image.overrideSprite = m_ButtonSprites[0];
    }

    public void ButtonClick()
    {
        Application.Quit();
    }
}
