using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerzookaLaugh : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }

    // This method can be called from an Animation Event named "BerzookaLaugh"
    public void BerzookaLaughAnimEvent()
    {
        PlayAudio();
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
