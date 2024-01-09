using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;   // Bullet speed.
    public int damage = 20;     // Damage dealt by the bullet.

    private void Update()
    {
        // Move the bullet forward.
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the bullet has hit an enemy.
        if (other.CompareTag("Enemy"))
        {
            // Get the EnemyAI component from the enemy.
            EnemyAI enemy = other.GetComponent<EnemyAI>();

            // Apply damage to the enemy.
            if (enemy != null)
            {
                // Attempt to get the PommeBlock script from the enemy
                PommeBlock pommeBlock = enemy.GetComponent<PommeBlock>();

                // If PommeBlock script exists and is blocking, attempt to override
                if (pommeBlock != null && pommeBlock.IsBlocking())
                {
                    // Trigger an animation event to resume taking damage
                    pommeBlock.SendMessage("ResumeDamage", SendMessageOptions.DontRequireReceiver);
                }

                // Apply damage to the enemy
                enemy.TakeDamage(damage);
            }

            // Destroy the bullet.
            Destroy(gameObject);
        }
        // Check if the bullet has hit the player.
        else if (!other.CompareTag("Player"))
        {
            // Check if the bullet has hit the "DamageArea" collider.
            if (other.CompareTag("DamageArea"))
            {
                // Attempt to get the PommeBlock script from the parent (enemy)
                PommeBlock pommeBlock = other.GetComponentInParent<PommeBlock>();

                // If PommeBlock script exists and is blocking, attempt to override
                if (pommeBlock != null && pommeBlock.IsBlocking())
                {
                    // Trigger an animation event to resume taking damage
                    pommeBlock.SendMessage("ResumeDamage", SendMessageOptions.DontRequireReceiver);
                }
            }

            // Destroy the bullet if it hits any other object (e.g., walls, obstacles).
            Destroy(gameObject);
        }
    }
}
