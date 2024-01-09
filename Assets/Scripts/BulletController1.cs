using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController1 : MonoBehaviour
{
    public float speed = 10f;   // Bullet speed.
    public int damage = 20;     // Damage dealt by the bullet.

    private void Update()
    {
        // Move the bullet forward.
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has hit an enemy.
        if (other.CompareTag("Player"))
        {
            // Get the EnemyAI component from the enemy.
            EnemyAI enemy = other.GetComponent<EnemyAI>();

            // Apply damage to the enemy.
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destroy the bullet.
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet if it hits any other object (e.g., walls, obstacles).
            Destroy(gameObject);
        }
    }
}
