using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMAD : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float attackAnimationDuration; // Public variable for the attack animation duration
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
    private bool isAttacking;
    private bool isDestroyed;

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

        if (isInAttackRange && !isDestroyed)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        anim.SetBool("isRunning", isInChaseRange && !isAttacking);
        anim.SetFloat("X", dir.x);
        anim.SetFloat("Y", dir.y);
        anim.SetBool("isAttacking", isAttacking);

        if (isAttacking && !isDestroyed)
        {
            StartCoroutine(DestroyAfterAttackAnimation());
        }
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

    private IEnumerator DestroyAfterAttackAnimation()
    {
        // Wait for the specified attack animation duration
        yield return new WaitForSeconds(attackAnimationDuration);

        // Now you can destroy the enemy object
        isDestroyed = true;
        Destroy(gameObject);
    }
}
