using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthAmount = 100;  // Health to be gained
    public AudioClip pickupSound;   // Sound effect to play on pickup

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component from the player GameObject
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Check if the PlayerHealth component is found
            if (playerHealth != null)
            {
                // Gain health
                playerHealth.GainHealth(healthAmount);

                // Play pickup sound effect
                PlayPickupSound();
            }

            // Destroy the health item GameObject
            Destroy(gameObject);
        }
    }

    private void PlayPickupSound()
    {
        // Check if an AudioClip is assigned
        if (pickupSound != null)
        {
            // Play the sound effect
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        else
        {
            Debug.LogWarning("Pickup sound not assigned to the HealthItem script.");
        }
    }
}
