using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameButtonScript : MonoBehaviour
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
        GlobalDataScript.ROOM_ENTRANCE = 0;
        GlobalDataScript.SetFragmentTags(Random.Range(1,6));
        SceneManager.LoadScene("S01");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(GlobalDataScript.LAST_SCENE);
    }
   
}
