using System.Collections;
using UnityEngine;

public class DashSFX : MonoBehaviour
{
    public AudioSource soundSource; // Drag and drop the AudioSource component from the Inspector

    // This method is called from the Animation Event named OnDodgeRollAnimationEvent
    public void PlayDashSFX()
    {
        // Check if the AudioSource and AudioClip are assigned
        if (soundSource != null && soundSource.clip != null)
        {
            // Play the sound effect
            soundSource.Play();
        }
        else
        {
            // Print a message to indicate that the AudioSource or AudioClip is missing
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }
}
