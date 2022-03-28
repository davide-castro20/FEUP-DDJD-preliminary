using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarScript : MonoBehaviour
{

    private float _counter = 1;
    private float _currentTime;

    [SerializeField] private GameObject bananas;
    [SerializeField] private GameObject pencils;
        
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _counter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePencilAmmo(String newAmmo)
    {
        Debug.Log(pencils.GetComponents<TextMeshPro>());
        pencils.GetComponent<TextMeshProUGUI>().text = newAmmo;
    }
}
