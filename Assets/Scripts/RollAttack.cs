using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttack : MonoBehaviour
{
    public float damageRadius = 5f; // Adjust the radius based on your preference
    public int damageAmount = 10; // Adjust the damage amount based on your preference

    private bool damageApplied = false;

    public void DamageEnemiesInRadius()
    {
        // Ensure damage is applied only once per animation event
        if (!damageApplied)
        {
            // Get all GameObjects with the tag "Enemy" in the specified radius
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageRadius, LayerMask.GetMask("Enemy"));

            // Damage each enemy within the radius
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                // Assuming the EnemyAI script is responsible for handling enemy health
                EnemyAI enemyAI = enemyCollider.GetComponent<EnemyAI>();

                if (enemyAI != null)
                {
                    enemyAI.TakeDamage(damageAmount);
                }
            }

            damageApplied = true; // Set the flag to true to indicate that damage has been applied
        }
    }

    // Animation event to reset the damageApplied flag
    public void ResetDamageFlag()
    {
        damageApplied = false;
    }

    // Draw the radius in the Scene view for visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
