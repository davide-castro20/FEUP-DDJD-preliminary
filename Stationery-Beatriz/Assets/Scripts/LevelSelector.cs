using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [Serializable]
    public struct Level
    {
        public string sceneName;
        public string title;
        public string description;
        public Sprite thumbnail;
        public bool available;
    }

    public GameObject CellPrefab;
    
    [SerializeField] private GameObject cellContainer;
    
    [SerializeField] private Level[] levels;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Level level in levels)
        {
            GameObject obj = Instantiate(CellPrefab, cellContainer.transform, false);
            obj.GetComponent<LevelCell>().InitCell(level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
