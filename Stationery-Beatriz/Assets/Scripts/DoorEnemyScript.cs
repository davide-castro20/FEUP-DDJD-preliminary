using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnemyScript : MonoBehaviour
{
    
    [SerializeField] private float openRate = 5;
    [SerializeField] private float timeOpen = 1;
    
    private PlayerScript _ps; 
    private float _currentTime = 0;
    private float _timeOpen = 0;
    private bool _isBlocked;
    private bool _isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        _isBlocked = false;
        _isOpen = false;
        _currentTime = openRate;
        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _currentTime -= Time.deltaTime;
        if (_isOpen) //Reduce time it door is open
        {
            _timeOpen -= Time.deltaTime;
            if (_timeOpen <= 0) //Close door
            {
                _isOpen = false;
                _currentTime = openRate;
            }
        }
        if (_currentTime <= 0 && !_isOpen) //Open door if time is reached and door is closed
        {
            _isOpen = true;
            _timeOpen = timeOpen;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            _isBlocked = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BottomCollider"))
        {
            if (_isOpen && !_isBlocked)
            {
                if (_ps.IsDisguised())
                {
                    _ps.RemoveDisguise(true);
                }
                else
                {
                    _ps.Kill();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            _isBlocked = false;
        }
    }
}
