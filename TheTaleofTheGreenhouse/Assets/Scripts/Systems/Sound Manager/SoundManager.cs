using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public bool test = false;
    
    [SerializeField] 
    private List<SoundClip> soundClips = new List<SoundClip>();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool PlaySound(string soundName)
    {
        foreach (var soundClip in soundClips)
        {
            if (soundClip.name == soundName)
            {
                audioSource.clip = Tools.GetRandomSound(soundClip.audioClip);
                audioSource.volume = soundClip.volume;
                audioSource.Play();
                return true;
            }
        }

        return false;
    }
}
