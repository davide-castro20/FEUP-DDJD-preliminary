using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;

    public static int DisguiseUses = 0;
    public static int BananaUses = 0;
    public static int EnemiesKilled = 0;
    public static float TimePassed = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else 
        {
            Destroy(this);
        }
        
        // DontDestroyOnLoad(gameObject);
    }

    public static GameData Instance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        TimePassed += Time.deltaTime;
    }

    public void StartLevel()
    {
        DisguiseUses = 0;
        BananaUses = 0;
        EnemiesKilled = 0;
        TimePassed = 0;
    }

    public static int CalculateScore()
    {
        var score = 1000 - (DisguiseUses * 20) - (BananaUses * 10) - (EnemiesKilled * 2) - (TimePassed);
        if (score < 0)
            return 0;
        return (int)score;
    }
}
