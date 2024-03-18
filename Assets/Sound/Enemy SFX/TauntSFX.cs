using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSFX : MonoBehaviour
{
    public AudioSource audioSource; // Assign your audio source in the Inspector
    public bool playOnce = false; // Toggle this in the Inspector to play sound only once

    private bool hasPlayed = false; // Flag to keep track of whether the sound has been played

    // This method will be called from the animation event
    public void PlayTauntSFX()
    {
        if (audioSource != null)
        {
            // Check if the audio source is not already playing
            if (!audioSource.isPlaying)
            {
                // Check if the sound should only be played once and if it has already been played
                if (playOnce && hasPlayed)
                {
                    return; // If so, do not play the sound again
                }

                audioSource.Play();
                hasPlayed = true; // Set the flag to true indicating the sound has been played
            }
        }
    }
}
