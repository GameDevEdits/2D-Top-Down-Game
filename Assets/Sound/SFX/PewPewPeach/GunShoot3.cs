using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot3 : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    // Animation event method to play audio 1
    public void PlayAudio1()
    {
        if (audioSource1 != null && audioSource1.clip != null)
        {
            audioSource1.Play();
        }
    }

    // Animation event method to play audio 2
    public void PlayAudio2()
    {
        if (audioSource2 != null && audioSource2.clip != null)
        {
            audioSource2.Play();
        }
    }

    // Animation event method to play audio 3
    public void PlayAudio3()
    {
        if (audioSource3 != null && audioSource3.clip != null)
        {
            audioSource3.Play();
        }
    }
}
