using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    // Damage amount to apply to the player
    public int damageAmount = 50;

    // Cooldown time in seconds
    public float cooldownTime = 1.0f;

    // Timer to track cooldown
    private float cooldownTimer = 0.0f;

    private void Update()
    {
        // Update the cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    // Method called when another collider enters the trigger collider attached to the object this script is attached to
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with has the tag "Player"
        if (collision.gameObject.CompareTag("Player") && cooldownTimer <= 0)
        {
            // Get the PlayerHealth component from the player object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // If the PlayerHealth component is found, apply damage to the player
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

                // Reset the cooldown timer
                cooldownTimer = cooldownTime;
            }
        }
    }
}
