using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private AudioManager _audioManager;
    private Rigidbody2D _rb;
    private bool grounded;
    private bool dragging;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuScript.GamePaused)
        {
            if (dragging)
            {
                dragging = false;
                _audioManager.StopSound("BoxDrag");
            }

            return;
        }
        
        if (Mathf.Abs(_rb.velocity.x) > 0.1f && grounded)
        {
            dragging = true;
            _audioManager.PlaySoundLoop("BoxDrag");
        }
        else if (dragging)
        {
            dragging = false;
            Debug.Log("Stopping box drag");
            _audioManager.StopSound("BoxDrag");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("box collided with " + collision.gameObject);
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("Box on ground");
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            Debug.Log("Box off the ground");
            grounded = false;
        }
    }
}
