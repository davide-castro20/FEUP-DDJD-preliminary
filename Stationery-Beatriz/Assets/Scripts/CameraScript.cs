using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float upperVerticalBound;
    [SerializeField] private float lowerVerticalBound;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        var playerPos = _playerTransform.position;
        var cameraPos = transform.position;

        var cameraX = cameraPos.x;
        var cameraY = cameraPos.y;
        
        if (playerPos.y > (cameraPos.y + upperVerticalBound))
        {
            cameraY = playerPos.y - upperVerticalBound;
        } 
        else if(playerPos.y < (cameraPos.y - lowerVerticalBound))
        {
            cameraY = playerPos.y + lowerVerticalBound;
        }


        if (playerPos.x < (cameraPos.x - leftBound))
        {
            cameraX = playerPos.x + leftBound;
        }
        else if (playerPos.x > (cameraPos.x + rightBound))
        {
            cameraX = playerPos.x - leftBound;
        }
        
        
        transform.position = new Vector3(cameraX, cameraY, cameraPos.z);
    }
}
