using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public static AudioClip GetRandomSound(AudioClip[] audioClip)
    {
        if (audioClip.Length == 0)
        {
            Debug.LogError("AudioClip does not contain any audio files");
            return null;
        }
        
        int random = Random.Range(0, audioClip.Length);

        return audioClip[random];
    }
}
