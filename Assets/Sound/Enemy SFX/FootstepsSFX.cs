using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : MonoBehaviour
{
    public AudioSource audioSource; // Assign your audio source in the Inspector

    // This method will be called from the animation event
    public void PlayFootstepsSFX()
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
