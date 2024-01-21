using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;   // Bullet speed.
    public int damage = 20;     // Damage dealt by the bullet.

    private Animator bulletAnimator;
    private Rigidbody2D rb;
    private bool isMoving = true;  // Flag to track if the bullet should be moving.
    private bool hasHitEnemy = false;  // Flag to track if the bullet has hit an enemy.

    public float explodeAnimationDuration = 1.0f; // Adjust this value based on your animation duration.
    public float bulletTime = 0.09f; // Time until the bullet is destroyed

    private void Start()
    {
        // Get the Animator and Rigidbody2D components from the bullet.
        bulletAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Start the coroutine to handle "rippleHit" after bullet time is over.
        StartCoroutine(SetRippleHitAfterBulletTime());
    }

    private void Update()
    {
        // Move the bullet forward only if it is allowed to move.
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the bullet has hit an enemy.
        if (other.CompareTag("Enemy") && !hasHitEnemy)
        {
            // Get the EnemyAI component from the enemy.
            EnemyAI enemy = other.GetComponent<EnemyAI>();

            // Stop the bullet movement immediately.
            isMoving = false;

            // Apply damage to the enemy.
            if (enemy != null)
            {
                // Attempt to get the PommeBlock script from the enemy.
                PommeBlock pommeBlock = enemy.GetComponent<PommeBlock>();

                // If PommeBlock script exists and is blocking, attempt to override.
                if (pommeBlock != null && pommeBlock.IsBlocking())
                {
                    // Trigger an animation event to resume taking damage.
                    pommeBlock.SendMessage("ResumeDamage", SendMessageOptions.DontRequireReceiver);
                }

                // Apply damage to the enemy.
                enemy.TakeDamage(damage);

                // Set the "normalHit" parameter to true for the explode animation.
                bulletAnimator.SetBool("normalHit", true);

                // Set the flag to true to indicate that the bullet has hit an enemy.
                hasHitEnemy = true;

                // Destroy the bullet after the animation is complete.
                StartCoroutine(DestroyAfterAnimation());
            }
        }
        // Check if the bullet has hit anything other than the player.
        else if (!other.CompareTag("Player"))
        {
            // Stop the bullet movement immediately.
            isMoving = false;

            // Set the "rippleHit" parameter to true for the explode animation.
            bulletAnimator.SetBool("rippleHit", true);

            // Freeze the bullet during animation playback.
            FreezeBullet();

            // Destroy the bullet after the animation is complete.
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for the duration of the explode animation.
        yield return new WaitForSeconds(explodeAnimationDuration);

        // Teleport the bullet to the specified position if it hasn't hit anything.
        if (!hasHitEnemy)
        {
            TeleportBulletToWhipImpactPosition();
        }

        // Destroy the bullet after teleportation.
        Destroy(gameObject);
    }

    private IEnumerator SetRippleHitAfterBulletTime()
    {
        // Wait for the bullet time duration.
        yield return new WaitForSeconds(bulletTime);

        // Set the "rippleHit" parameter to true after bullet time is over.
        bulletAnimator.SetBool("rippleHit", true);

        // Freeze the bullet during animation playback.
        FreezeBullet();
    }

    private void FreezeBullet()
    {
        // Disable the Rigidbody2D component to freeze the bullet.
        rb.velocity = Vector2.zero;  // Stop the bullet's current velocity.
        isMoving = false;
    }

    private void TeleportBulletToWhipImpactPosition()
    {
        // Find the GameObject with the name "WhipImpactPosition" in the scene.
        GameObject whipImpactPosition = GameObject.Find("WhipImpactPosition");

        // Check if the GameObject is found.
        if (whipImpactPosition != null)
        {
            // Teleport the bullet to the position of "WhipImpactPosition".
            transform.position = whipImpactPosition.transform.position;
        }
        else
        {
            // Print a warning if "WhipImpactPosition" is not found.
            Debug.LogWarning("GameObject with name 'WhipImpactPosition' not found in the scene.");
        }
    }
}
