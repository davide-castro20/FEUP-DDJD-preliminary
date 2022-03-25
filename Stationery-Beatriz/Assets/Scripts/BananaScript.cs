using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBanana(int playerDirection, float throwForce)
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(playerDirection * throwForce, throwForce);
        _rb.AddForce(force);
    }
}
