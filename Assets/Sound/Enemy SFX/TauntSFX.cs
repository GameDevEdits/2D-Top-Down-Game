using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSFX : MonoBehaviour
{
    public AudioSource audioSource; // Assign your audio source in the Inspector

    // This method will be called from the animation event
    public void PlayTauntSFX()
    {
        if (audioSource != null)
        {
            // Check if the audio source is not already playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
