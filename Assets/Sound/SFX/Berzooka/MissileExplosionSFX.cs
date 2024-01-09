using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosionSFX : MonoBehaviour
{
    // Assuming you have assigned this AudioSource in the Unity Editor
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }

    public void MissileExplosionSFXAnimEvent()
    {
        // Stop all audio sources on the GameObject
        StopAllAudioSources();

        // Play the audio for the current event
        PlayAudio();
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void StopAllAudioSources()
    {
        // Get all AudioSources on the GameObject
        AudioSource[] audioSources = GetComponents<AudioSource>();

        // Stop all currently playing audio sources
        foreach (var source in audioSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}
