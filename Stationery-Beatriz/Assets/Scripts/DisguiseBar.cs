using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisguiseBar : MonoBehaviour
{
    private GameObject _player;
    private Slider _slider;
    
    [SerializeField] private float offsetFromPlayer = 2f;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject border;
    
    private float _duration = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        var abovePlayer = _player.transform.position;
        abovePlayer.y += offsetFromPlayer;
        
        var wantedPos = Camera.main.WorldToScreenPoint (abovePlayer);
        transform.position = wantedPos;
    }

    public void ActivateDisguise(float duration)
    {
        _duration = duration;
        _slider.enabled = true;
        border.SetActive(true);
    }

    public void SetProgress(float current)
    {
        _slider.value = current / _duration;
    }

    public void DeactivateDisguise()
    {
        _slider.value = 0;
        _slider.enabled = false;
        border.SetActive(false);
    }
}
