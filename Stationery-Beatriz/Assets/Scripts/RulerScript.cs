using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.transform.parent.GetComponent<Enemy>();
            
            if(enemy != null) 
                enemy.Kill();
        }
    }
}
