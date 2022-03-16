using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorScript : MonoBehaviour
{
    [SerializeField] private float leftOffset;
    [SerializeField] private float rightOffset;
    [SerializeField] private int direction; // -1 -> left first; 1 -> right first
    [SerializeField] private float speed;
    
    private Vector3 _initialPos;
    private Vector3 _leftPos;
    private Vector3 _rightPos;
    private Vector3 _currentTarget;
    private PlayerScript _ps;

    // Start is called before the first frame update
    void Start()
    {
        _initialPos = gameObject.transform.position;
        _leftPos = new Vector3(_initialPos.x - leftOffset, _initialPos.y, _initialPos.z);
        _rightPos = new Vector3(_initialPos.x + rightOffset, _initialPos.y, _initialPos.z);

        _currentTarget = direction < 0 ? _leftPos : _rightPos;

        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == _rightPos)
        {
            _currentTarget = _leftPos;
        }
        else if (transform.position == _leftPos)
        {
            _currentTarget = _rightPos;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _ps.Kill();
        }
    }
}
