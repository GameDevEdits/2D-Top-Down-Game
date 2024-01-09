using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMAD : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;

    private GameObject player;
    private Vector3 shootingDirection;

    // Function to be called as an Animation Event
    public void DamageShoot()
    {
        // Ensure the bulletPrefab is set
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned in NewEnemyMRA script.");
            return;
        }

        // Find the player GameObject (you can adjust this logic based on your game)
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Calculate the shooting direction towards the player
            shootingDirection = player.transform.position - transform.position;

            // Create a bullet instance
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Apply force to the bullet to move it in the calculated direction
                bulletRb.velocity = shootingDirection.normalized * bulletSpeed; // Adjust bullet speed
            }

            // Apply damage to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Assuming player has a HealthController script
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bulletDamage);
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }
}