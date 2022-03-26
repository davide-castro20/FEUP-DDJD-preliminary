using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackColliderScript : MonoBehaviour
{
    [SerializeField] private GameObject _banana;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) 
            Destroy(_banana);
        Debug.Log(col.gameObject.tag);
    }
}
