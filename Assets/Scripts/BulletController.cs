using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;   // Bullet speed.
    public int damage = 25; // Base damage dealt by the bullet.
    public static float criticalHitChance = 0.2f; // Chance for a critical hit (20%).

    public static int originalDamage = 25; // Static variable to store the original damage, initialized to 25.

    private Animator bulletAnimator;
    private Rigidbody2D rb;
    private bool isMoving = true;  // Flag to track if the bullet should be moving.
    private bool hasHitEnemy = false;  // Flag to track if the bullet has hit an enemy.

    public float explodeAnimationDuration = 1.0f; // Adjust this value based on your animation duration.
    public float bulletTime = 0.09f; // Time until the bullet is destroyed

    private void Awake()
    {
        // Ensure originalDamage is initialized correctly and persists across scene changes.
        if (originalDamage == 0)
        {
            originalDamage = damage;
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset originalDamage when the scene is loaded (game restarts).
        originalDamage = 25; // Set the original damage value explicitly if needed
    }

    private void Start()
    {
        // Get the Animator and Rigidbody2D components from the bullet.
        bulletAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Start the coroutine to handle "rippleHit" after bullet time is over.
        StartCoroutine(SetRippleHitAfterBulletTime());

        // Check for a critical hit.
    }

    private void CheckForCriticalHit()
    {
        // Generate a random value between 0 and 1.
        float randomValue = Random.value;

        // Check if the random value is within the critical hit chance.
        if (randomValue <= criticalHitChance)
        {
            // Apply the critical hit by multiplying the base damage.
            damage *= 2;

            // Set the "criticalHit" parameter to true for the explode animation.
            bulletAnimator.SetBool("criticalHit", true);
            Debug.Log("Crit!");
        }
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

                CheckForCriticalHit();

                // Check if the enemy has the "Block" parameter set to true in its animator.
                Animator enemyAnimator = enemy.GetComponent<Animator>();
                bool enemyIsBlocking = enemyAnimator.GetBool("Block");

                // Play the appropriate hit animation based on whether the enemy is blocking.
                if (enemyIsBlocking)
                {
                    // Set the "rippleHit" parameter to true for the explode animation.
                    bulletAnimator.SetBool("rippleHit", true);
                }
                else
                {
                    // Set the "normalHit" parameter to true for the explode animation.
                    bulletAnimator.SetBool("normalHit", true);
                }

                // Apply damage to the enemy.
                enemy.TakeDamage(damage);

                // Set the flag to true to indicate that the bullet has hit an enemy.
                hasHitEnemy = true;

                // Destroy the bullet after the animation is complete.
                StartCoroutine(DestroyAfterAnimation());
            }
        }
        // Check if the bullet has hit anything other than the player.
        else if (!other.CompareTag("Player"))
        {
            // Check if the bullet has hit anything other than an enemy (e.g., "Tree").
            // If it has, ignore the collision and return early.
            return;
        }
        else
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

    public static void IncreaseCriticalHitChance(float amount)
    {
        criticalHitChance += amount;
    }

    public static void ResetDamage()
    {
        // Reset damage to the original value
        originalDamage = 25; // Set the original damage value explicitly if needed
    }
}
