using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColour : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // The color to change to (pure white)
    public Color targetColor = Color.white;

    // Duration of the color change effect in seconds
    public float colorChangeDuration = 1f;

    void Start()
    {
        // Get the SpriteRenderer component on the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if SpriteRenderer is present
        if (spriteRenderer != null)
        {
            // Change the sprite color to the target color
            spriteRenderer.color = targetColor;

            // Start the Coroutine to revert the color
            StartCoroutine(RevertColorCoroutine());
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }
    }

    IEnumerator RevertColorCoroutine()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(colorChangeDuration);

        // Reset the sprite color to the original color
        spriteRenderer.color = Color.white; // Change this if the original color is different
    }
}
