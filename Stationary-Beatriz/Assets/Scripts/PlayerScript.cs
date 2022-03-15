using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private BoxCollider2D bottom_collider;
    
    private bool grounded = true;
    private Rigidbody2D model;
    

    private float hMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            model.AddForce(new Vector2(model.velocity.x, jumpSpeed));
            grounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    void FixedUpdate()
    {
        model.velocity = new Vector2(hMove * speed, model.velocity.y);
    }

    public void ResetJump()
    {
        Debug.Log("Reset Jump");
        grounded = true;
    }

    public void SetAirborne()
    {
        Debug.Log("Set Airborne");
        grounded = false;
    }
}
