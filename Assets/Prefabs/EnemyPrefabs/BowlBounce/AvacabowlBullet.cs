using System.Collections;
using UnityEngine;

public class AvacabowlBullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private int damage;

    public Animator animator;
    private bool hasCollided = false;

    public void Init(Vector2 dir, float bulletSpeed, int bulletDamage)
    {
        direction = dir;
        speed = bulletSpeed;
        damage = bulletDamage;

        // Assuming you have an Animator component attached to your missile GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if "Explode" is true before continuing the movement
        if (animator != null && !animator.GetBool("Explode"))
        {
            MoveBullet();
        }
    }

    void MoveBullet()
    {
        // Move the bullet in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided) return; // If already collided, exit

        if (other.CompareTag("Player"))
        {
            HandleCollision(other.gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            // Reverse the horizontal direction of the bullet
            direction = new Vector2(-direction.x, direction.y);
        }
        else if (!other.CompareTag("Enemy"))
        {
            HandleCollision(null);
        }

        // Check if the other collider has a script named "HomingBullet" and disable it
        HomingBullet homingBulletScript = other.GetComponent<HomingBullet>();
        if (homingBulletScript != null)
        {
            homingBulletScript.enabled = false;
        }
    }

    void HandleCollision(GameObject player)
    {
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        DisableColliderAndFreezePosition();

        StartCoroutine(DelayedSortingOrderChange());

        // Trigger explosion animation if the collision is not with a wall
        if (animator != null)
        {
            animator.SetBool("Explode", true);
            hasCollided = true; // Set a flag to ensure the explosion animation is not triggered multiple times

            // Start the coroutine to wait for the explosion animation to finish before destroying the GameObject
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            // Log a warning if the Animator component is not found
            Debug.LogWarning("Animator component not found on the missile GameObject.");
            Destroy(gameObject);
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
    }

    IEnumerator DelayedSortingOrderChange()
    {
        yield return null;  // Wait for the next frame.

        // Change sorting order.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder += 500;
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for the length of the explosion animation
        yield return new WaitForSeconds(2.0f);

        // Destroy the GameObject after the animation is finished
        Destroy(gameObject);
    }
}
