using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchwayAudioSFX : MonoBehaviour
{
    public AudioSource audioSource1; // Assign your first audio source in the Inspector
    public AudioSource audioSource2; // Assign your second audio source in the Inspector

    private bool canPlayAudio = false;
    private float delayDuration = 0.5f;

    private void Start()
    {
        // Start the delay coroutine
        StartCoroutine(EnableAudioAfterDelay());
    }

    private IEnumerator EnableAudioAfterDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        canPlayAudio = true;
    }

    // This method will be called from the animation event to play the first audio
    public void PlayAOpenAudio()
    {
        if (canPlayAudio)
        {
            PlayAudio(audioSource1);
        }
    }

    // This method will be called from the animation event to play the second audio
    public void PlayACloseAudio()
    {
        if (canPlayAudio)
        {
            PlayAudio(audioSource2);
        }
    }

    private void PlayAudio(AudioSource audioSource)
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