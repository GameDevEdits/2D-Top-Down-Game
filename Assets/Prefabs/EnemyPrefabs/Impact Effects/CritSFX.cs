using UnityEngine;

public class CritSFX : MonoBehaviour
{
    public AudioSource audioSource;

    // Call this method from your animation event
    public void CritSFXPlay()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not set.");
        }
    }
}
