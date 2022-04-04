using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioManager _audioManager;
    private bool _dead = false;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("1"))
        {
            Destroy(this.gameObject);
        }
    }

    public void Kill()
    {
        _dead = true;
        _audioManager.PlaySound("Poof");
        _animator.Play("Death");
    }

    public bool getDead()
    {
        return this._dead;
    }
}
