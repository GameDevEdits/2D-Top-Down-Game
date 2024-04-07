using System.Collections;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int bulletDamage = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
            }

            // Disable the collider and freeze the position
            DisableColliderAndFreezePosition();
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            DisableColliderAndFreezePosition();
        }
        else if (other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
        {
            // Disable the collider and freeze the position
            DisableColliderAndFreezePosition();
        }
    }

    void DisableColliderAndFreezePosition()
    {
        // Disable the collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Freeze the position
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop rigidbody movement
            rb.gravityScale = 0f; // Disable gravity (if applicable)
            rb.isKinematic = true; // Make the rigidbody kinematic
        }

        // Start the coroutine to wait for 2 seconds before destroying the bullet
        StartCoroutine(DestroyBulletAfterDelay());
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // Destroy the bullet after the delay
        Destroy(gameObject);
    }
}