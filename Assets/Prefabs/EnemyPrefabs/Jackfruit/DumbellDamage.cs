using System.Collections;
using UnityEngine;

public class DumbellDamage : MonoBehaviour
{
    public int bulletDamage = 50;

    private Animator animator;
    private Collider2D collider;
    private bool hasImpacted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
        if (collider == null)
        {
            Debug.LogError("Collider component not found!");
        }
    }

    void Update()
    {
        if (animator != null && !hasImpacted && animator.GetBool("Impact"))
        {
            // Start the coroutine to disable the collider and freeze position after 1 second
            StartCoroutine(DisableColliderAndFreezePositionAfterDelay(0.5f));
            hasImpacted = true; // Set flag indicating impact
        }
    }

    private IEnumerator DisableColliderAndFreezePositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Disable the collider
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
            }
        }
    }
}
