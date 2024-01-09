using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    public float flashDuration = 0.1f;
    public Color flashColor = Color.red;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            // Handle the enemy being hit by a player bullet.
            StartCoroutine(FlashEnemy());
        }
    }

    IEnumerator FlashEnemy()
    {
        // Flash the enemy with the specified color for the specified duration.
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // Reset the enemy's color to its original color.
        spriteRenderer.color = originalColor;
    }
}
