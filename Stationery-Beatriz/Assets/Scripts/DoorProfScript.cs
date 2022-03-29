using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProfScript : MonoBehaviour
{
    [SerializeField] private int _visionRangeRight;
    [SerializeField] private int _visionRangeLeft;
    [SerializeField] private GameObject area;
    
    private Animator _anim;

    private GameObject _player;

    private Vector3 _thisPos;
    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _thisPos = this.transform.position;
        Transform areaTransform = area.transform;
        

        area.transform.localScale = new Vector3(_visionRangeLeft + _visionRangeRight, area.transform.localScale.y, 1);
        var position = areaTransform.localPosition;
        area.transform.localPosition = new Vector3(_visionRangeLeft - _visionRangeRight, position.y, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("DoorOpened"))
        {
            Vector3 playerPos = _player.transform.position;
            if (playerPos.x < _thisPos.x + _visionRangeRight && playerPos.x > _thisPos.x - _visionRangeLeft)
            {
                if (Math.Abs(playerPos.y - _thisPos.y) < 1)
                {
                    PlayerScript _ps = _player.GetComponent<PlayerScript>();
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Box"))
            _anim.SetBool("NotBlocked", false);
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Box"))
            _anim.SetBool("NotBlocked", true);
    }
}
