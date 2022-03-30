using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProfArea : MonoBehaviour
{
    [SerializeField] private GameObject areaLeft;

    [SerializeField] private GameObject areaRight;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetArea(float rangeLeft, float rangeRight)
    {
        var transform1 = areaLeft.transform;
        Vector3 areaScale = transform1.localScale;
        areaScale.x = rangeLeft;
        transform1.localScale = areaScale;

        var localPosition = transform1.localPosition;
        localPosition = new Vector3(- rangeLeft / 2.0f, localPosition.y, localPosition.z);
        transform1.localPosition = localPosition;
        
        
        var transform2 = areaRight.transform;
        areaScale = transform1.localScale;
        areaScale.x = rangeRight;
        transform2.localScale = areaScale;

        localPosition = transform1.localPosition;
        localPosition = new Vector3(rangeRight / 2.0f, localPosition.y, localPosition.z);
        transform2.localPosition = localPosition;
    }
}
