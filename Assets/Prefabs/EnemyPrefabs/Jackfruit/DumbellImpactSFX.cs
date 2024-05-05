using UnityEngine;

public class DumbellImpactSFX : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    // Method to be called by the animation event
    public void DumbellFallImpactSFX()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the audio clip
        }
        else
        {
            Debug.LogWarning("AudioSource component not assigned!");
        }
    }
}
