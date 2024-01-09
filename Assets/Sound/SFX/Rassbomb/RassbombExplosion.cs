using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RassbombExplosion : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }

    public void RassbombExplosionSFXAnimEvent()
    {
        PlayAudio();
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
