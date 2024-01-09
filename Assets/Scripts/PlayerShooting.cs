using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;    // Reference to the bullet prefab.
    public float bulletSpeed = 10f;   // Speed of the bullet.
    public int damagePerShot = 20;     // Amount of damage per shot.

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check for player input to shoot (e.g., left mouse button).
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get the mouse position in world coordinates.
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set the z-coordinate to zero (2D game).

        // Calculate the direction from the player to the mouse cursor.
        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        // Instantiate a bullet at the player's position.
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody2D component from the bullet.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet to shoot in the calculated direction.
        rb.velocity = shootDirection * bulletSpeed;

        // Set the damage for the bullet.
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.damage = damagePerShot;
    }
}
