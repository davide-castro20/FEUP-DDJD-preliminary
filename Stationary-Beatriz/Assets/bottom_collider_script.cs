using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottom_collider_script : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D bc;

    private player_script _ps;
    // Start is called before the first frame update
    void Start()
    {
        _ps = bc.gameObject.GetComponent<player_script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _ps.ResetJump();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _ps.SetAirborne();
        }
    }
}
