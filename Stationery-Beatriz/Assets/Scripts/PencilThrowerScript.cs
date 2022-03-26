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

    private float _currentTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = throwRate;
        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            ProjectileScript ps = gameObject.GetComponent<ProjectileScript>();
            ps.StartProjectile(range * direction, speed);
            ps.setEnemyProjectile(true);
            _currentTime = throwRate;
            _animator.SetTrigger("Throw");
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
