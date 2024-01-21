using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 0.5f; // Adjust this value for the desired fire rate
    public float bulletTime = 3f; // Time until the bullet is destroyed

    private bool canShoot = true;
    public AudioSource shootingAudioSource;
    public AudioClip[] shootingSoundEffects; // Array to hold multiple audio clips
    private int currentAudioIndex = 0;

    private bool spaceKeyPressed = false;

    private void Start()
    {
        // Get the AudioSource component from the GameObject
        shootingAudioSource = GetComponent<AudioSource>();
    }

    // This method can be called from an Animation Event
    public void StartShootingAnimation()
    {
        CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
        Shoot();
        StartCoroutine(FireRateCooldown());
        PlayShootingSound();
    }

    // This method can be called from an Animation Event
    public void OnSpaceKeyAnimationEvent()
    {
        if (spaceKeyPressed)
        {
            CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
            spaceKeyPressed = false; // Reset the flag after the camera shake
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

        // Attach a script to the bullet to handle collisions
        bullet.AddComponent<BulletCollisionHandler>();

        // Don't destroy the bullet immediately, let the BulletController handle it.
    }

    IEnumerator FireRateCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f / fireRate);
        canShoot = true;
    }

    void PlayShootingSound()
    {
        // Check if the AudioSource and AudioClip array are assigned
        if (shootingAudioSource != null && shootingSoundEffects != null && shootingSoundEffects.Length > 0)
        {
            // Play the current audio clip
            shootingAudioSource.PlayOneShot(shootingSoundEffects[currentAudioIndex]);

            // Increment the index for the next audio clip
            currentAudioIndex = (currentAudioIndex + 1) % shootingSoundEffects.Length;
        }
        else
        {
            // Print a message to indicate that the AudioSource or AudioClip array is missing
            Debug.LogWarning("AudioSource or AudioClip array is not assigned.");
        }
    }
}

public class BulletCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Shield"
        if (other.CompareTag("Shield"))
        {
            // Destroy the bullet without dealing damage
            Destroy(gameObject);
        }
    }
}
