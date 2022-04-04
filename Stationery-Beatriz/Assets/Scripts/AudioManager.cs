using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private SoundEffect[] _soundEffects;

    public AudioManager instance;
    

    void Awake()
    {
        foreach (SoundEffect s in _soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaySound("Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clipName)
    {
        SoundEffect s = Array.Find(_soundEffects, sound => sound.name == clipName);

        if (s == null)
        {
            Debug.Log("Invalid audio clip name");
            return;
        }
        
        Debug.Log("Playing " + clipName);
        s.source.Play();
    }

    public void StopSound(string clipName)
    {
        SoundEffect s = Array.Find(_soundEffects, sound => sound.name == clipName);

        if (s == null)
        {
            Debug.Log("Invalid audio clip name");
            return;
        }
        
        s.source.Stop();
    }
}
