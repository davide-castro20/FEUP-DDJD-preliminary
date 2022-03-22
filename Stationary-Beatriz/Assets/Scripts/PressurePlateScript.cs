using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bridge;

    protected BridgeScript _bs;
    
    [SerializeField] 
    protected List<String> _plateReseters;

    protected int nColisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        _bs = bridge.GetComponent<BridgeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_plateReseters.Contains(col.gameObject.tag))
        {
            _bs.Activate();
            nColisions++;
        }
    }
}
