using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public enum Ammo
    {
        Banana,
        Pencil,
        Glider,
        Ruler
    }

    [SerializeField] 
    private Ammo ammoType; 
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private AudioManager _audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReloadPlayer(other);
            _audioManager.PlaySound("PickUp");
        }
    }

    private void ReloadPlayer(Collider2D player)
    {
        player.gameObject.GetComponent<PlayerScript>().AddAmmo(ammoType, 1);
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
}

