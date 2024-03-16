using UnityEngine;
using System.Collections;

public class StarfruitAttack : MonoBehaviour
{
    public float initialFreezeDuration = 2f; // Time to freeze position at the start
    public float chaseRadius = 5f;
    public float attackRadius = 2f;
    public float damageRadius = 7f;
    public float speed = 5f;
    public float dashSpeedMultiplier = 2f;
    public int dashDamage = 50; // Damage per dash
    public int maxDamagePerCycle = 50; // Maximum damage per attack animation cycle

    private Transform target;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isRunning;
    private bool isAttacking;
    private bool isDashing;
    private bool hasDamagedPlayerThisCycle; // Flag to track if the player has already been damaged during the current cycle

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Freeze position at the start for initialFreezeDuration seconds
        StartCoroutine(FreezePosition(initialFreezeDuration));
    }

    private void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= chaseRadius)
        {
            isRunning = true;
            isAttacking = distanceToTarget <= attackRadius;

            if (isAttacking)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isRunning", false);

                // Reset damage flag at the start of each attack animation cycle
                hasDamagedPlayerThisCycle = false;
            }
            else
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", true);
            }
        }
        else
        {
            isRunning = false;
            isAttacking = false;
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
        }
    }

    private bool IsPlayerWithinDamageRadius()
    {
        return Vector2.Distance(transform.position, target.position) <= damageRadius;
    }

    private void FixedUpdate()
    {
        if (isRunning && !isAttacking && !isDashing)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Flip the enemy based on player position
        if (direction.x > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Animation event to start the dash
    public void Charge()
    {
        // Freeze position
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    // Animation event to perform the dash
    public void StarDash()
    {
        // Unfreeze position
        rb.constraints = RigidbodyConstraints2D.None;

        // Dash towards the player
        Vector2 dashDirection = (target.position - transform.position).normalized;
        rb.velocity = dashDirection * speed * dashSpeedMultiplier;

        isDashing = true;
    }

    // Animation event to end the dash
    public void EndDash()
    {
        // Revert back to normal
        rb.constraints = RigidbodyConstraints2D.None;
        isDashing = false;
    }

    // Animation event to apply damage to the player
    public void ApplyDamage()
    {
        if (!hasDamagedPlayerThisCycle && IsPlayerWithinDamageRadius())
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                int damageToApply = Mathf.Min(dashDamage, maxDamagePerCycle);
                playerHealth.TakeDamage(damageToApply);
                hasDamagedPlayerThisCycle = true;
            }
        }
    }

    private IEnumerator FreezePosition(float duration)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw chase radius
        DrawRadius(chaseRadius, Color.red);

        // Draw attack radius
        DrawRadius(attackRadius, Color.blue);

        // Draw damage radius
        DrawRadius(damageRadius, Color.yellow);
    }

    private void DrawRadius(float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}