using UnityEngine;

public class SpawnSound : MonoBehaviour
{
    public AudioSource soundSource;

    // Called from an Animation Event
    public void SpawnSoundPlay()
    {
        if (soundSource != null)
        {
            soundSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource is not assigned in the SpawnSound script on GameObject: " + gameObject.name);
        }
    }
}
