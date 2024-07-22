using UnityEngine;

public class TreebornRevival : MonoBehaviour
{
    public int revivalIncrement = 25; // Public variable to set the revival increment

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                IncreaseRevivalChance(playerHealth);
            }
        }
    }

    private void IncreaseRevivalChance(PlayerHealth playerHealth)
    {
        playerHealth.SetRevivalChance(playerHealth.GetRevivalChance() + revivalIncrement);
    }
}
