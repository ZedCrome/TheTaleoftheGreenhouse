using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClip
{
    public string name;
    [Range(.0f, 1.0f)]
    public float volume;
    public AudioClip[] audioClip;
}
