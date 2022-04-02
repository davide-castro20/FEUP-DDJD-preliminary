using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bridge;

    [SerializeField]
    protected Sprite not_pressed;
    [SerializeField]
    protected Sprite pressed;
    
    protected SpriteRenderer _spriteRenderer;

    
    protected BridgeScript _bs;
    
    [SerializeField] 
    protected List<String> _plateReseters;

    protected int nColisions = 0;


    // Start is called before the first frame update
    void Start()
    {
        _bs = bridge.GetComponent<BridgeScript>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = not_pressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_plateReseters.Contains(col.gameObject.tag))
        {
            _spriteRenderer.sprite = pressed;
            _bs.Activate();
            nColisions++;
        }
    }
}
