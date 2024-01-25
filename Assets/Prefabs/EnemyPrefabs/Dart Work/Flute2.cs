using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flute2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public float spreadAngle = 25f;
    private GameObject player;

    private bool hasShot = false;  // Flag to track shooting state

    void Start()
    {
        // Assign the player GameObject at the start
        player = GameObject.FindWithTag("Player");
    }

    // This method is called by the animation event to trigger shooting
    void FluteShootAnimPlay()
    {
        // Check if the player is null or shooting has already occurred
        if (player == null || hasShot)
        {
            return;
        }

        // Calculate direction towards the player
        Vector2 directionToPlayer = player.transform.position - transform.position;

        // Calculate the angle between the two darts
        float angleBetweenDarts = spreadAngle * 2;

        // Calculate rotations for the darts
        Quaternion rotation1 = Quaternion.AngleAxis(-spreadAngle, Vector3.forward);
        Quaternion rotation2 = Quaternion.AngleAxis(spreadAngle, Vector3.forward);

        // Spawn the first dart
        GameObject dart1 = Instantiate(bulletPrefab, firePoint.position, rotation1);
        dart1.GetComponent<DartScript>().Initialize(directionToPlayer.normalized, bulletSpeed);

        // Spawn the second dart
        GameObject dart2 = Instantiate(bulletPrefab, firePoint.position, rotation2);
        dart2.GetComponent<DartScript>().Initialize(directionToPlayer.normalized, bulletSpeed);

        // Set the flag to prevent further shooting until the next animation event
        hasShot = true;
    }

    // This method is called by the animation event to reset the shooting state
    void ResetFluteFlag()
    {
        hasShot = false;
    }
}
