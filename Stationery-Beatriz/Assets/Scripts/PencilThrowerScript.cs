using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilThrowerScript : MonoBehaviour
{
    [SerializeField] private float throwRate = 2;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int direction;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    
    private PlayerScript _ps; 
    private Animator _animator;
    private Enemy _enemyScript;


    private float _currentTime = 0;

    private bool _throwAnim = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = throwRate;
        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
        _enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enemyScript.getDead())
        {
            _currentTime -= Time.deltaTime;

            // start animation before actually throwing the pencil
            if (_currentTime <= 0.5f && !_throwAnim)
            {
                _animator.SetTrigger("Throw");
                _throwAnim = true;
            }

            if (_currentTime <= 0)
            {
                GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
                ProjectileScript ps = gameObject.GetComponent<ProjectileScript>();
                ps.StartProjectile(range * direction, speed);
                ps.setEnemyProjectile(true);
                _currentTime = throwRate;
                _throwAnim = false;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BottomCollider"))
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
