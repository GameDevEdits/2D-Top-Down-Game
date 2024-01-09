using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileSFX : MonoBehaviour
{
    public AudioSource audioSource;

    // Flag to check if the sound has been played
    private bool hasPlayed = false;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }

    public void HomingMissileSFXAnimEvent()
    {
        // Check if the sound has already been played
        if (!hasPlayed)
        {
            PlayAudio();
            hasPlayed = true;
        }
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
