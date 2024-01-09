using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMRAVisual : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;

    public Transform firePoint; // Assign the firepoint transform in the Unity Inspector

    private GameObject player;
    private Vector3 shootingDirection;

    // Function to be called as an Animation Event
    public void VisualShoot()
    {
        // Ensure the bulletPrefab and firePoint are set
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned in NewMRAVisual script.");
            return;
        }
        if (firePoint == null)
        {
            Debug.LogError("Fire Point is not assigned in NewMRAVisual script.");
            return;
        }

        // Find the player GameObject (you can adjust this logic based on your game)
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Calculate the shooting direction towards the player
            shootingDirection = player.transform.position - transform.position;

            // Create a bullet instance at the firepoint position
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Access the BulletLookAtPlayer script and initialize it with the player's position
            BulletLookAtPlayer bulletLookAtPlayer = bullet.GetComponent<BulletLookAtPlayer>();
            if (bulletLookAtPlayer != null)
            {
                bulletLookAtPlayer.Initialize(player.transform);
            }

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Calculate the direction from the firepoint to the player
                shootingDirection = player.transform.position - firePoint.position;

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
