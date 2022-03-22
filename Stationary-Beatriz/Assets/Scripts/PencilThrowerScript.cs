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
    
    
    private float _currentTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = throwRate;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            gameObject.GetComponent<ProjectileScript>().StartProjectile(range * direction, speed);
            _currentTime = throwRate;
        }
        
    }
}
