using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    private AudioManager _audioManager;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _animator = GetComponent<Animator>();
        _audioManager.PlaySound("Poof");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Fds");
        Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).ToString());
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("1"))
        {
            Destroy(this.gameObject);
        }
    }
}
