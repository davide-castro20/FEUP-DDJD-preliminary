using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    public static bool GamePaused;
    public bool levelComplete;
    private GameObject _pauseMenu;
    private GameObject _levelCompleteMenu;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = gameObject.transform.Find("PauseMenu").gameObject;
        _levelCompleteMenu = gameObject.transform.Find("CompleteMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GamePaused && !levelComplete)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    public void LevelComplete()
    {
        levelComplete = true;
        GamePaused = true;
        Time.timeScale = 0f;
        _levelCompleteMenu.SetActive(true);
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
