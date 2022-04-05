using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    private AudioManager _audioManager;
    public static bool GamePaused;
    private bool levelComplete;
    private GameObject _pauseMenu;
    private GameObject _levelCompleteMenu;
    private GameObject _deathMenu;
    

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _pauseMenu = gameObject.transform.Find("PauseMenu").gameObject;
        _levelCompleteMenu = gameObject.transform.Find("CompleteMenu").gameObject;
        _deathMenu = gameObject.transform.Find("DeathMenu").gameObject;
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

    private void Awake()
    {
        Time.timeScale = 1;
        GamePaused = false;
    }

    public void LevelComplete()
    {
        levelComplete = true;
        GamePaused = true;
        Time.timeScale = 0f;
        var score = GameData.CalculateScore();
        CompleteMenuScript cms = _levelCompleteMenu.GetComponent<CompleteMenuScript>();
        cms.SetScore(score);
        cms.SetDisguises(GameData.DisguiseUses);
        cms.SetBananas(GameData.BananaUses);
        cms.SetEnemies(GameData.EnemiesKilled);
        cms.SetTime(GameData.TimePassed);
        _levelCompleteMenu.SetActive(true);
    }

    public void DeathScreen()
    {
        GamePaused = true;
        Time.timeScale = 0f;
        _deathMenu.SetActive(true);
        
        _audioManager.StopAllSounds();
        _audioManager.StopSound("Theme");
        _audioManager.PlaySound("DeathScreen");
    }
    
    private void PauseGame()
    {
        if (levelComplete) return;
        
        GamePaused = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        
        _audioManager.PauseSound("Theme");
    }
    
    public void ResumeGame()
    {
        GamePaused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _audioManager.UnPauseSound("Theme");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }
    
    public void ExitToMainMenu()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
