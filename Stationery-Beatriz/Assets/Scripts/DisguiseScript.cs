using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DisguiseScript : MonoBehaviour
{
    [SerializeField] 
    private float _disguiseTime;

    [SerializeField] private float _timeToReappear;

    private float _countdown;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _countdown = 0;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisguisePlayer(other);
        }
    }

    private void DisguisePlayer(Collider2D player)
    {
        player.gameObject.GetComponent<PlayerScript>().Disguise(_disguiseTime);
        _countdown = _timeToReappear;
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_countdown > 0)
        {
            _countdown -= Time.deltaTime;
            if (_countdown <= 0)
            {
                _spriteRenderer.enabled = true;
                _boxCollider2D.enabled = true;
            }
        }
    }
}
