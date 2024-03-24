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
            // Reflect the direction of the bullet upon collision with a wall
            ReflectOffWall(other);
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
        DisableColliderAndFreezePosition();

        StartCoroutine(DelayedSortingOrderChange());

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

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void ReflectOffWall(Collider2D wallCollider)
    {
        // Get the contact points of collision
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        wallCollider.GetContacts(contacts);

        if (contacts.Length > 0)
        {
            // Calculate the normal of the collision surface
            Vector2 wallNormal = contacts[0].normal;

            // Calculate the velocity vector of the bullet
            Vector2 velocity = direction.normalized * speed;

            // Calculate the reflected velocity
            Vector2 reflectedVelocity = Vector2.Reflect(velocity, wallNormal);

            // Update the direction of the bullet to the reflected velocity
            direction = reflectedVelocity.normalized;

            // Move the bullet slightly away from the wall to avoid sticking
            transform.position = contacts[0].point + (wallNormal * 0.01f);
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
