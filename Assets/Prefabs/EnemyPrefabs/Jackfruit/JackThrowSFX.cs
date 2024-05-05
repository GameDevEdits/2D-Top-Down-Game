using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackThrowSFX : MonoBehaviour
{
    public AudioSource audioSource;

    // This method will be called from the animation event
    public void ThrowDumbellSFX()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio source is not assigned!");
        }
    }
}
