using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public static AudioClip GetRandomSound(AudioClip[] audioClip)
    {
        int random = Random.Range(0, audioClip.Length);

        return audioClip[random];
    }
}
