using UnityEngine;

public class DisableEnableScripts : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableScriptsAndAudio();
        }
    }

    public void EnableScriptsAndAudio()
    {
        if (targetObject != null)
        {
            // Enable all scripts on the target object
            MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }

            // Enable all audio sources on the target object
            AudioListener audioListener = targetObject.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = true;
            }

            AudioSource[] audioSources = targetObject.GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.enabled = true;
            }
        }
    }

    void DisableScriptsAndAudio()
    {
        if (targetObject != null)
        {
            // Disable all scripts on the target object
            MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            // Disable all audio sources on the target object
            AudioListener audioListener = targetObject.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }

            AudioSource[] audioSources = targetObject.GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.enabled = false;
            }
        }
    }
}
