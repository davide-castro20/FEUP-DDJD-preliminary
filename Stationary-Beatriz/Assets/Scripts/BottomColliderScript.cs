using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomColliderScript : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D bc;

    private PlayerScript _ps;
    // Start is called before the first frame update
    void Start()
    {
        _ps = bc.gameObject.GetComponent<PlayerScript>();
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
