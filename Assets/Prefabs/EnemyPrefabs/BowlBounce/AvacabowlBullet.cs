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
            HandlePlayerCollision(other.gameObject);
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            HandlePlayerBulletCollision(other.gameObject);
        }
        else if (!other.CompareTag("PlayerBullet") && !other.CompareTag("Player"))
        {
            // Handle collision with walls, enemies, or obstacles by reversing direction
            direction = new Vector2(-direction.x, direction.y);
        }
    }

    void HandlePlayerCollision(GameObject player)
    {
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Trigger explosion animation
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

    void HandlePlayerBulletCollision(GameObject playerBullet)
    {
        // Trigger explosion animation
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

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for the length of the explosion animation
        yield return new WaitForSeconds(2.0f);

        // Destroy the GameObject after the animation is finished
        Destroy(gameObject);
    }
}
