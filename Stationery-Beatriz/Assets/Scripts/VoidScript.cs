using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidScript : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().Kill();
        }
        if (other.CompareTag("Banana"))
        {
            Destroy(GameObject.Find("Banana(Clone)"));
        }
    }
}
