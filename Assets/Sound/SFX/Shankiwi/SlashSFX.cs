using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public NewEnemyMADShank enemyScript;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }

        // Find the enemy script if not assigned
        if (enemyScript == null)
        {
            enemyScript = GetComponentInParent<NewEnemyMADShank>();
            if (enemyScript == null)
            {
                Debug.LogError("NewEnemyMADShank script not found!");
            }
        }
    }

    public void SlashAnimEvent()
    {
        // Check if the player is within the damage radius before playing audio
        if (enemyScript != null && enemyScript.IsPlayerWithinDamageRadius())
        {
            PlayAudio();
        }
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
