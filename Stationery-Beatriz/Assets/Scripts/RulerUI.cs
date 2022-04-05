using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulerUI : MonoBehaviour
{
    [SerializeField] private Sprite full;
    [SerializeField] private Sprite half;

    private Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(int state)
    {
        Debug.Log(state);
        switch (state)
        {
            case 0:
                SetNone();
                break;
            case 1:
                SetHalf();
                break;
            case 2:
                SetFull();
                break;
            default:
                SetNone();
                break;
        }  
    }
    
    public void SetFull()
    {
        _image.sprite = full;
        _image.color = Color.white;
    }

    public void SetHalf()
    {
        _image.sprite = half;
        _image.color = Color.white;
    }

    public void SetNone()
    {
        _image.sprite = half;
        _image.color = new Color(1,1,1,0.4f);
    }
}
