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
    
    private bool _grounded = true;
    private Rigidbody2D _model;
    private Vector3 _spawnPoint;
    
    private float _hMove = 0f;

    private Animator _animator;
    private float _disguiseTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _model = GetComponent<Rigidbody2D>();
        _spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        _animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        _hMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _grounded == true)
        {
            _model.AddForce(new Vector2(_model.velocity.x, jumpSpeed));
            _grounded = false;
        }

        if (_disguiseTime > 0)
        {
            if ((_disguiseTime -= Time.deltaTime) < 0)
                RemoveDisguise();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    void FixedUpdate()
    {
        _model.velocity = new Vector2(_hMove * speed, _model.velocity.y);
    }

    public void ResetJump()
    {
        // Debug.Log("Reset Jump");
        _grounded = true;
    }

    public void SetAirborne()
    {
        // Debug.Log("Set Airborne");
        _grounded = false;
    }

    public void Kill()
    {
        transform.position = _spawnPoint;
    }

    public void Disguise(float duration)
    {
        _animator.SetBool("Disguised", true);
        _disguiseTime = duration;
    }

    public void RemoveDisguise()
    {
        _animator.SetBool("Disguised", false);
    }
}
