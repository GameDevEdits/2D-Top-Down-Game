using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float speed = 3f;           // Speed at which the enemy moves towards the player.
    public int maxHealth = 100;        // Maximum health of the enemy.
    public int currentHealth;          // Current health of the enemy.

    private Transform player;           // Reference to the player's Transform.

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Move towards the player.
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Function to handle enemy taking damage.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the enemy's health has reached zero or below.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to destroy the enemy.
    void Die()
    {
        // Destroy the enemy GameObject.
        Destroy(gameObject);
    }
}
