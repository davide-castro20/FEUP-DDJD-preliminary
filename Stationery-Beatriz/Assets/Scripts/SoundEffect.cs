using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public string name;
    
    public AudioClip audioClip;

    public float volume;

    public AudioSource source;

    public bool loop;
}
