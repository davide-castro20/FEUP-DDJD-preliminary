using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanAreaScript : MonoBehaviour
{
    [SerializeField] 
    private GameObject janitor;

    private JanitorScript _janitorScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _janitorScript = janitor.GetComponent<JanitorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Banana"))
        {
            _janitorScript.CleanBanana(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Banana"))
        {
            _janitorScript.LeftBanana();
        }
    }
}
