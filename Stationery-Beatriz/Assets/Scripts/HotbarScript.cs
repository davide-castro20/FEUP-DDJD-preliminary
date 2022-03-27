using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HotbarScript : MonoBehaviour
{

    private float _counter = 1;
    private float _currentTime;
    
    
    private Button[] _hotbarSlots;
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _counter;
        _hotbarSlots = GetComponentsInChildren<Button>();
        //Debug.Log(_hotbarSlots[1].GetComponents<Text>());
        //Debug.Log(_hotbarSlots[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePencilAmmo(String newAmmo)
    {
        _hotbarSlots[1].GetComponentInChildren<Text>().text = newAmmo;
    }
}
