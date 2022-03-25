using System;
using System.Collections.Generic;
using UnityEngine;

public class BottomColliderScript : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D bc;

    [SerializeField] 
    private List<String> _jumpReseters;

    private PlayerScript _ps;
    private int nColisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        _ps = bc.gameObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_jumpReseters.Contains(other.gameObject.tag))
        {
            nColisions++;
            _ps.ResetJump();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_jumpReseters.Contains(other.gameObject.tag))
        {
            if(--nColisions == 0)
                _ps.SetAirborne();
        }
    }
}
