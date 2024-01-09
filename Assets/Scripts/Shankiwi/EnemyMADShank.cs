using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMADShank : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float avoidanceRadius; // Set the desired avoidance radius.

    public LayerMask whatIsPlayer;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    public int bulletDamage;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private float lastShotTime;
    public float shootInterval = 1f;

    private bool isAttacking; // Added to track if the enemy is attacking

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;

        // Avoidance behavior
        AvoidOtherEnemies();

        // Set animation based on whether the enemy is attacking
        if (isInAttackRange)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        anim.SetBool("isRunning", isInChaseRange && !isAttacking);

        // Update the animator parameters
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
        anim.SetBool("isAttacking", isAttacking);
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isAttacking)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
            Shoot();
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    private void Shoot()
    {
        if (Time.time - lastShotTime >= shootInterval)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BFire bulletComponent = bullet.GetComponent<BFire>();

            if (bulletComponent != null)
            {
                bulletComponent.Init(dir.normalized, bulletSpeed, bulletDamage);
            }

            lastShotTime = Time.time;
        }
    }

    // Function to avoid other enemies within the specified radius.
    private void AvoidOtherEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.transform != transform)
            {
                // Calculate avoidance vector and normalize it.
                Vector2 avoidVector = transform.position - collider.transform.position;
                avoidVector.Normalize();

                // Adjust the movement direction to avoid the nearby enemy.
                movement += avoidVector;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

}