using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public GameObject orange1, orange2, orange3, orange4, orange5;
    public GameObject slice1, slice2, slice3, slice4, slice5;

    public AudioSource orangeDisableAudioSource;
    public AudioSource sliceDisableAudioSource;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        int currentHealth = playerHealth.currentHealth;

        // Orange 1
        if (currentHealth > 450)
        {
            EnableObject(orange1, orangeDisableAudioSource);
            DisableObject(slice1, sliceDisableAudioSource);
        }
        else if (currentHealth <= 450 && currentHealth > 400)
        {
            DisableObject(orange1, orangeDisableAudioSource);
            EnableObject(slice1, sliceDisableAudioSource);
        }
        else
        {
            DisableObject(orange1, orangeDisableAudioSource);
            DisableObject(slice1, sliceDisableAudioSource);
        }

        // Orange 2
        if (currentHealth > 350)
        {
            EnableObject(orange2, orangeDisableAudioSource);
            DisableObject(slice2, sliceDisableAudioSource);
        }
        else if (currentHealth <= 350 && currentHealth > 300)
        {
            DisableObject(orange2, orangeDisableAudioSource);
            EnableObject(slice2, sliceDisableAudioSource);
        }
        else
        {
            DisableObject(orange2, orangeDisableAudioSource);
            DisableObject(slice2, sliceDisableAudioSource);
        }

        // Orange 3
        if (currentHealth > 250)
        {
            EnableObject(orange3, orangeDisableAudioSource);
            DisableObject(slice3, sliceDisableAudioSource);
        }
        else if (currentHealth <= 250 && currentHealth > 200)
        {
            DisableObject(orange3, orangeDisableAudioSource);
            EnableObject(slice3, sliceDisableAudioSource);
        }
        else
        {
            DisableObject(orange3, orangeDisableAudioSource);
            DisableObject(slice3, sliceDisableAudioSource);
        }

        // Orange 4
        if (currentHealth > 150)
        {
            EnableObject(orange4, orangeDisableAudioSource);
            DisableObject(slice4, sliceDisableAudioSource);
        }
        else if (currentHealth <= 150 && currentHealth > 100)
        {
            DisableObject(orange4, orangeDisableAudioSource);
            EnableObject(slice4, sliceDisableAudioSource);
        }
        else
        {
            DisableObject(orange4, orangeDisableAudioSource);
            DisableObject(slice4, sliceDisableAudioSource);
        }

        // Orange 5
        if (currentHealth > 50)
        {
            EnableObject(orange5, orangeDisableAudioSource);
            DisableObject(slice5, sliceDisableAudioSource);
        }
        else if (currentHealth <= 50 && currentHealth > 0)
        {
            DisableObject(orange5, orangeDisableAudioSource);
            EnableObject(slice5, sliceDisableAudioSource);
        }
        else
        {
            DisableObject(orange5, orangeDisableAudioSource);
            DisableObject(slice5, sliceDisableAudioSource);
        }
    }

    void EnableObject(GameObject obj, AudioSource audioSource)
    {
        if (obj != null && !obj.activeSelf)
        {
            obj.SetActive(true);
            PlaySound(audioSource);
        }
    }

    void DisableObject(GameObject obj, AudioSource audioSource)
    {
        if (obj != null && obj.activeSelf)
        {
            obj.SetActive(false);
            PlaySound(audioSource);
        }
    }

    void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // Update the UI continuously (you might want to optimize this based on your game requirements)
        UpdateHealthUI();
    }
}