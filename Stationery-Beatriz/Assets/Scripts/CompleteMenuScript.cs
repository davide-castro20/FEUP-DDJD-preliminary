using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteMenuScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI disguiseText;
    [SerializeField]
    private TextMeshProUGUI bananaText;
    [SerializeField]
    private TextMeshProUGUI enemiesText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetDisguises(int disguiseUses)
    {
        disguiseText.text = disguiseUses + " x -20";
    }

    public void SetBananas(int bananaUses)
    {
        bananaText.text = bananaUses + " x -10";
    }

    public void SetEnemies(int enemiesKilled)
    {
        enemiesText.text = enemiesKilled + " x -2";
    }

    public void SetTime(float timePassed)
    {
        timeText.text = (int)timePassed + " x -1";
    }
}
