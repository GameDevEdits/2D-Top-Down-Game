using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackfruitAttack : MonoBehaviour
{
    public float initialFreezeDuration = 2f; // Time to freeze speed at the start
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float avoidanceRadius;
    public float startThrowRadius;
    public float endThrowRadius;
    public float shootInterval = 1f;
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
    private bool isAttacking;
    private bool isThrowing;

    private float lastShotTime;
    private float originalSpeed; // Store the original speed before freezing

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        // Store the original speed
        originalSpeed = speed;

        // Freeze speed at the start for initialFreezeDuration seconds
        StartCoroutine(FreezeSpeed(initialFreezeDuration));
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);
        bool isInThrowRange = Physics2D.OverlapCircle(transform.position, startThrowRadius, whatIsPlayer) &&
                              !Physics2D.OverlapCircle(transform.position, endThrowRadius, whatIsPlayer);

        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;

        AvoidOtherEnemies();

        if (isInThrowRange && !isThrowing)
        {
            isThrowing = true;
            anim.SetBool("isThrowing", isThrowing);
        }
        else if (!isInThrowRange && isThrowing)
        {
            isThrowing = false;
            anim.SetBool("isThrowing", isThrowing);
        }
        else if (isInAttackRange)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        anim.SetBool("isRunning", isInChaseRange && !isAttacking && !isThrowing);
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
        anim.SetBool("isAttacking", isAttacking);
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isAttacking && !isThrowing)
        {
            MoveCharacter(movement);
        }
        else if (isInAttackRange && !isThrowing)
        {
            rb.velocity = Vector2.zero;
            Shoot();
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        // Check if the initial freeze duration has passed
        if (Time.timeSinceLevelLoad > initialFreezeDuration)
        {
            rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
        }
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

    private void AvoidOtherEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.transform != transform)
            {
                Vector2 avoidVector = transform.position - collider.transform.position;
                avoidVector.Normalize();
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

        // Draw start throw radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, startThrowRadius);

        // Draw end throw radius
        Gizmos.DrawWireSphere(transform.position, endThrowRadius);
    }

    private IEnumerator FreezeSpeed(float duration)
    {
        speed = 0f; // Freeze the speed
        yield return new WaitForSeconds(duration);
        speed = originalSpeed; // Unfreeze the speed after the duration
    }
}
