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

    public void StartBanana(int playerDirection, float throwForce,double angle)
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2((int)(playerDirection * throwForce * Math.Cos(angle)), (int)(throwForce * Math.Sin(angle)));
        _rb.AddForce(force);
    }
}
