using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;
    public float dartSpreadAngle = 30f; // Angle between the two darts
    public float verticalOffset = 0.5f; // Vertical offset for the darts

    public Transform firePoint; // Assign the firepoint transform in the Unity Inspector

    private GameObject player;

    // Function to be called as an Animation Event
    public void FluteVisualShoot()
    {
        // Ensure the bulletPrefab and firePoint are set
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned in FluteShoot script.");
            return;
        }
        if (firePoint == null)
        {
            Debug.LogError("Fire Point is not assigned in FluteShoot script.");
            return;
        }

        // Find the player GameObject (you can adjust this logic based on your game)
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Calculate the shooting direction towards the player
            Vector3 shootingDirection = player.transform.position - transform.position;

            // Calculate the spread angles for the two darts
            float halfSpreadAngle = dartSpreadAngle / 2f;

            // Determine the relative position of the player to the enemy
            Vector3 playerRelativePosition = player.transform.position - transform.position;

            // Determine the sign for horizontal and vertical offsets based on player's position
            float horizontalOffsetSign = Mathf.Sign(playerRelativePosition.x);
            float verticalOffsetSign = Mathf.Sign(playerRelativePosition.y);

            // Calculate the shooting direction for the left dart with offsets
            Quaternion leftDartRotation = Quaternion.AngleAxis(-halfSpreadAngle, Vector3.forward);
            Vector3 leftDartOffset = Quaternion.Euler(0f, 0f, -halfSpreadAngle) * new Vector3(horizontalOffsetSign * 0.5f, verticalOffsetSign * verticalOffset, 0f);
            Vector3 leftDartPosition = firePoint.position + leftDartOffset;
            GameObject leftDart = Instantiate(bulletPrefab, leftDartPosition, leftDartRotation);
            SetupDart(leftDart, shootingDirection);

            // Calculate the shooting direction for the right dart with offsets
            Quaternion rightDartRotation = Quaternion.AngleAxis(halfSpreadAngle, Vector3.forward);
            Vector3 rightDartOffset = Quaternion.Euler(0f, 0f, halfSpreadAngle) * new Vector3(horizontalOffsetSign * 0.5f, verticalOffsetSign * verticalOffset, 0f);
            Vector3 rightDartPosition = firePoint.position + rightDartOffset;
            GameObject rightDart = Instantiate(bulletPrefab, rightDartPosition, rightDartRotation);
            SetupDart(rightDart, shootingDirection);

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

    // Setup common configurations for the dart
    private void SetupDart(GameObject dart, Vector3 shootingDirection)
    {
        // Access the BulletLookAtPlayer script and initialize it with the player's position
        BulletLookAtPlayer bulletLookAtPlayer = dart.GetComponent<BulletLookAtPlayer>();
        if (bulletLookAtPlayer != null)
        {
            bulletLookAtPlayer.Initialize(player.transform);
        }

        Rigidbody2D dartRb = dart.GetComponent<Rigidbody2D>();

        if (dartRb != null)
        {
            // Apply force to the dart to move it in the calculated direction
            dartRb.velocity = shootingDirection.normalized * bulletSpeed; // Adjust bullet speed
        }
    }
}
