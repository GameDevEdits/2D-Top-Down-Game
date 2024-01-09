using UnityEngine;

public class DeathAudioChange : MonoBehaviour
{
    public AudioSource audioSource1; // Assign in the Inspector
    public AudioSource audioSource2; // Assign in the Inspector
    public AudioClip audio2Clip; // Assign in the Inspector

    private void Start()
    {
        // Ensure that both AudioSources are initially enabled or disabled as needed
        if (audioSource1 != null)
        {
            audioSource1.enabled = true; // Set to false if needed
        }

        if (audioSource2 != null)
        {
            audioSource2.enabled = false; // Set to true if needed
        }
    }

    // Animation event method to switch audio sources and play audio2
    public void SwitchAudioSourcesAndPlayAudio2()
    {
        // Ensure that both AudioSources are assigned
        if (audioSource1 != null && audioSource2 != null)
        {
            // Disable the first AudioSource and enable the second AudioSource
            audioSource1.enabled = false;
            audioSource2.enabled = true;

            // Play audio2 if the AudioClip is assigned
            if (audio2Clip != null)
            {
                audioSource2.PlayOneShot(audio2Clip);
            }
            else
            {
                // Print a warning if audio2Clip is not assigned
                Debug.LogWarning("Audio2 AudioClip is not assigned.");
            }
        }
        else
        {
            // Print a message if either AudioSource is not assigned
            Debug.LogError("One or both AudioSources are not assigned.");
        }
    }
}

