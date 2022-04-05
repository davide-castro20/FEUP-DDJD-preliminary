using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _timeToLive = 10;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _timeToLive -= Time.deltaTime;
        if (_timeToLive <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void StartBanana(int playerDirection, float throwForce)
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2((int)(playerDirection * throwForce), (int)(throwForce));
        _rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _rb.velocity = Vector3.zero;
        }
    }
}
