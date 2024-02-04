using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSurvivalRadius : MonoBehaviour
{
    public float radius = 5f; // Set the default radius in the inspector

    private void Update()
    {
        // Check for all game objects with tag "PlayerBullet" in the scene
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        foreach (GameObject bullet in bullets)
        {
            // Check if the bullet is outside the radius
            if (Vector3.Distance(transform.position, bullet.transform.position) > radius)
            {
                // Check if "normalHit" or "criticalHit" is true in the bullet's animator
                Animator bulletAnimator = bullet.GetComponent<Animator>();
                if (bulletAnimator != null)
                {
                    bool normalHit = bulletAnimator.GetBool("normalHit");
                    bool criticalHit = bulletAnimator.GetBool("criticalHit");

                    // Destroy the bullet only if both "normalHit" and "criticalHit" are not true
                    if (!normalHit && !criticalHit)
                    {
                        Destroy(bullet);
                    }
                }
                else
                {
                    // Destroy the bullet if it doesn't have an Animator component
                    Destroy(bullet);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a purple wire sphere in the scene view to represent the radius
        Gizmos.color = new Color(0.5f, 0, 0.5f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

