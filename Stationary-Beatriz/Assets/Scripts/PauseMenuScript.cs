using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    public static bool GamePaused;
    private GameObject _pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = gameObject.transform.Find("PauseMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    private void PauseGame()
    {
        GamePaused = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        GamePaused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void ExitToMainMenu()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
