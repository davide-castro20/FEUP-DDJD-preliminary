using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ProjectileScript : MonoBehaviour
{
    private float _speed;
    private Vector3 _target;
    private bool _isInitialized = false;
    
    private PlayerScript _ps; 
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartProjectile(float range, float speed)
    {
        _speed = speed;
        _target = transform.position + new Vector3(range, 0, 0);
        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInitialized)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }

        if (transform.position == _target)
        {
            Destroy(gameObject);
        }
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
}
