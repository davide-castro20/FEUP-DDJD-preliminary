using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private Image thumbnail;

    private string _sceneName;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCell(LevelSelector.Level level)
    {
        title.text = level.title;
        description.text = level.description;
        thumbnail.sprite = level.thumbnail;
        _sceneName = level.sceneName;

        if (!level.available)
        {
            Destroy(GetComponent<Button>());
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
