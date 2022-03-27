using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float anglesPerSecond = 270f;
    
    private float _speed;
    private Vector3 _target;
    private bool _isInitialized = false;
    private Vector3 _rotationVector;
    
    private PlayerScript _ps;
    private bool _initiated = false;
    public bool enemyProjectile;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartProjectile(float range, float speed)
    {
        _speed = speed;
        if (range < 0)
        {
            _rotationVector = Vector3.forward;
        }
        else
        {
            _rotationVector = Vector3.back;
        }
        
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
            transform.Rotate(_rotationVector * Time.deltaTime * anglesPerSecond);
        }

        if (transform.position == _target)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_initiated)
        {
            if (enemyProjectile && other.gameObject.CompareTag("Player"))
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
            // Player sent projectile
            else if (!enemyProjectile && other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                Destroy(this.gameObject);
            }
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

    public void setEnemyProjectile(bool b)
    {
        enemyProjectile = b;
        _initiated = true;
    }
}
