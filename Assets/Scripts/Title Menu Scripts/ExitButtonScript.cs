using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    private Button m_ControlsButton;
    public Sprite[] m_ButtonSprites;


    void Start()
    {
        m_ControlsButton = GetComponent<Button>();
    }

    public void ButtonHover()
    {
        m_ControlsButton.image.overrideSprite = m_ButtonSprites[1];
    }

    public void ButtonNotHover()
    {
        m_ControlsButton.image.overrideSprite = m_ButtonSprites[0];
    }

    public void OpenControlsMenu()
    {
        Application.Quit();
    }
}
