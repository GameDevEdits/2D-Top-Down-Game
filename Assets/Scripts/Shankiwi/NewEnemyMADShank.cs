using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMADShank : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject slashPrefab; // Add reference to your slash prefab
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;
    public float damageRadius = 5f; // Set the desired damage radius.
    public float slashOffset = 1f; // Offset for spawning the slash above the player

    private GameObject player;
    private Vector3 shootingDirection;

    // Function to be called as an Animation Event to spawn the slash
    public void SlashVFX()
    {
        // Ensure the slashPrefab is set and the player is within the damage radius
        if (slashPrefab == null || !IsPlayerWithinDamageRadius())
        {
            return;
        }

        // Get the position to spawn the slash above the player
        Vector3 spawnPosition = player.transform.position + Vector3.up * slashOffset;

        // Enable the slash game object
        slashPrefab.SetActive(true);

        // Instantiate the slash prefab at the calculated spawn position
        GameObject slash = Instantiate(slashPrefab, spawnPosition, Quaternion.identity);

        // Parent the slash to the player
        slash.transform.parent = player.transform;
    }

    // Function to be called as an Animation Event to shoot the bullet and apply damage
    public void DamageShoot()
    {
        // Ensure the bulletPrefab is set and the player is within the damage radius
        if (bulletPrefab == null || !IsPlayerWithinDamageRadius())
        {
            return;
        }

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

        // Check enemy's position relative to player's position
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = player.transform.position;

        // Flip the slash game object if enemy is on the left side of the player
        if (enemyPosition.x < playerPosition.x)
        {
            FlipSlash(true); // Flip horizontally
        }
        else
        {
            FlipSlash(false); // Keep normal orientation
        }
    }

    // Check if the player is within the damage radius
    public bool IsPlayerWithinDamageRadius()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= damageRadius;
        }

        return false;
    }

    // Flip the slash game object horizontally
    void FlipSlash(bool flip)
    {
        if (slashPrefab != null)
        {
            Vector3 scale = slashPrefab.transform.localScale;
            scale.x = flip ? -1f : 1f; // Flip if true, keep normal if false
            slashPrefab.transform.localScale = scale;
        }
    }
}
