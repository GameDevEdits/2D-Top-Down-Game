using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PS : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet to be shot.
    public Transform firePoint; // Transform representing the gun's muzzle position.
    public GameObject muzzleFlash; // GameObject representing the muzzle flash effect.
    public float bulletForce = 10f; // Force applied to the bullet when fired.
    public float fireRate = 0.5f; // Rate of fire (bullets per second).

    private float nextFireTime = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Create and fire a bullet.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Play the muzzle flash effect.
        PlayMuzzleFlash();

        // Destroy the bullet after a certain time (adjust as needed).
        Destroy(bullet, 2f);
    }

    void PlayMuzzleFlash()
    {
        // Instantiate the muzzle flash effect at the gun's muzzle position.
        Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);

        // You can add additional logic here to control the duration or other properties of the muzzle flash.
    }
}
