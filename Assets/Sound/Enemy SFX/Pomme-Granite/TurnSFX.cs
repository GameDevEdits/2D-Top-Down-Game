using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSFX : MonoBehaviour
{
    public AudioSource audioSource; // Assign your audio source in the Inspector
    public FootstepsSFX footstepsSFX; // Reference to the FootstepsSFX script

    // This method will be called from the animation event
    public void PlayTurnSFX()
    {
        if (audioSource != null)
        {
            // Check if the audio source is not already playing
            if (!audioSource.isPlaying)
            {
                // Stop the footsteps sound effect if it is playing
                if (footstepsSFX != null && footstepsSFX.audioSource.isPlaying)
                {
                    footstepsSFX.audioSource.Stop();
                }
                audioSource.Play();
            }
        }
    }
}
