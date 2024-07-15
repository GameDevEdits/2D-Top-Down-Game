using System.Collections;
using UnityEngine;

public class DumbellDamage : MonoBehaviour
{
    public int bulletDamage = 50;

    private Animator animator;
    private Collider2D collider;
    private bool hasImpacted = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
        if (collider == null)
        {
            Debug.LogError("Collider component not found!");
        }
        else
        {
            collider.enabled = false; // Ensure collider is initially disabled
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    void Update()
    {
        if (animator != null && !hasImpacted && animator.GetBool("Impact"))
        {
            // Start the coroutine to disable the collider and freeze position after 0.5 second
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
        if (collider.enabled && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
            }
        }
    }

    // This method will be called by the animation event
    public void DoDumbellDamage()
    {
        // Enable the collider for the duration of the damage event
        if (collider != null)
        {
            collider.enabled = true;
            StartCoroutine(DisableColliderAfterDamage());
        }

        // Start the coroutine to fade out and disappear
        StartCoroutine(FadeOutAndDisappear(3f, 2f)); // Start fading out after 3 seconds, over 2 seconds
    }

    private IEnumerator DisableColliderAfterDamage()
    {
        // Wait for a very short duration to allow the damage to be dealt
        yield return new WaitForSeconds(0.1f);
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private IEnumerator FadeOutAndDisappear(float delay, float fadeDuration)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the GameObject is completely invisible
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Optionally, disable the GameObject after it has faded out
        gameObject.SetActive(false);
    }
}
