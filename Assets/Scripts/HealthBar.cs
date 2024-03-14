using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public GameObject orange1, orange2, orange3, orange4, orange5;
    public GameObject slice1, slice2, slice3, slice4, slice5;

    public AudioSource orangeDisableAudioSource;
    public AudioSource sliceDisableAudioSource;

    private void Start()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        int currentHealth = playerHealth.currentHealth;

        DisableObject(orange1, currentHealth > 450, orangeDisableAudioSource);
        DisableObject(slice1, currentHealth <= 450 && currentHealth > 400, sliceDisableAudioSource);

        DisableObject(orange2, currentHealth > 350, orangeDisableAudioSource);
        DisableObject(slice2, currentHealth <= 350 && currentHealth > 300, sliceDisableAudioSource);

        DisableObject(orange3, currentHealth > 250, orangeDisableAudioSource);
        DisableObject(slice3, currentHealth <= 250 && currentHealth > 200, sliceDisableAudioSource);

        DisableObject(orange4, currentHealth > 150, orangeDisableAudioSource);
        DisableObject(slice4, currentHealth <= 150 && currentHealth > 100, sliceDisableAudioSource);

        DisableObject(orange5, currentHealth > 50, orangeDisableAudioSource);
        DisableObject(slice5, currentHealth <= 50 && currentHealth > 0, sliceDisableAudioSource);
    }

    private void DisableObject(GameObject obj, bool condition, AudioSource audioSource)
    {
        if (obj != null && obj.activeSelf != condition)
        {
            StartCoroutine(DisableWithDelay(obj, condition, audioSource));
        }
    }

    private IEnumerator DisableWithDelay(GameObject obj, bool condition, AudioSource audioSource)
    {
        yield return new WaitForSeconds(0.1f); // Adjust delay time as needed

        obj.SetActive(condition);
        PlaySound(audioSource);
    }

    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        // Update the UI continuously (you might want to optimize this based on your game requirements)
        UpdateHealthUI();
    }
}
