using UnityEngine;

public class DestroyOnHealthZero : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        // Get the EnemyAI component attached to the same GameObject
        enemyAI = GetComponent<EnemyAI>();

        // Check if EnemyAI component is found
        if (enemyAI == null)
        {
            // If EnemyAI component is not found, log an error and disable this script
            Debug.LogError("EnemyAI component not found! This script requires an EnemyAI component to function.");
            enabled = false;
        }
    }

    private void Update()
    {
        // Check if the current health of the EnemyAI is 0 or below
        if (enemyAI != null && enemyAI.currentHealth <= 0)
        {
            // If health is 0 or below, destroy the GameObject
            Destroy(gameObject);
        }
    }
}
