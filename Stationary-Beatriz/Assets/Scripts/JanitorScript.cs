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
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _initialPos = gameObject.transform.position;
        _leftPos = new Vector3(_initialPos.x - leftOffset, _initialPos.y, _initialPos.z);
        _rightPos = new Vector3(_initialPos.x + rightOffset, _initialPos.y, _initialPos.z);

        if (direction < 0)
        {
            TurnLeft();
        }
        else
        {
            TurnRight();
        }

        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == _rightPos)
        {
            TurnLeft();

        }
        else if (transform.position == _leftPos)
        {
            TurnRight();
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_ps.IsDisguised())
            {
                _ps.RemoveDisguise(true);
            }
            else
            {
                _ps.Kill();
            }
        }
    }

    private void TurnLeft()
    {
        _currentTarget = _leftPos;
    }

    private void TurnRight()
    {
        _currentTarget = _rightPos;
    }
}
