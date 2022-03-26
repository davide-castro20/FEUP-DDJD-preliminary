using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReloadPlayer(other);
        }
    }

    private void ReloadPlayer(Collider2D player)
    {
        player.gameObject.GetComponent<PlayerScript>().AddAmmo(1);
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
}

