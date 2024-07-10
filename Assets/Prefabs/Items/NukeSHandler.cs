using UnityEngine;

public class NukeSHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("NukeS"))
        {
            HandleCollision();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("NukeS"))
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        // 50% chance to trigger the global isDead state
        if (Random.value <= 0.5f)
        {
            EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
            foreach (var enemy in enemies)
            {
                enemy.Die(); // Assuming you have made the Die method public in the EnemyAI script
            }
        }
    }
}
