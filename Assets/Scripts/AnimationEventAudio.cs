using UnityEngine;

public class AnimationEventAudio : MonoBehaviour
{
    public AudioSource audioSource;
    private bool hasAudioPlayed = false;

    // Call this method from the animation event with a parameter to control audio playback
    public void PlayAudio(int shouldPlay)
    {
        if (audioSource != null && shouldPlay == 1 && !hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        else if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }

    // Call this method from an animation event to reset the flag
    public void ResetAudioFlag()
    {
        hasAudioPlayed = false;
    }
}
