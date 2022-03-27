using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    [SerializeField] private int _speed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed);
 
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_speed);
            }
            else
            {
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }
}
