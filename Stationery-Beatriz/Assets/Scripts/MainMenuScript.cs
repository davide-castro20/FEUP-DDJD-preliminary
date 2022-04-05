using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public List<string> scenes;

    [SerializeField] private GameObject controlsScreen;
    
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(scenes[0]);
    }

    public void Controls()
    {
        controlsScreen.SetActive(true);
    }

    public void ExitControls()
    {
        controlsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
