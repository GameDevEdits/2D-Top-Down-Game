using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the Rigidbody2D component for checking velocity
        rb = GetComponent<Rigidbody2D>();

        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check the horizontal velocity of the enemy
        float horizontalVelocity = rb.velocity.x;

        // Flip the sprite if moving left and not already facing left
        if (horizontalVelocity < 0 && !spriteRenderer.flipX)
        {
            Flip();
        }
        // Flip the sprite back if moving right and not already facing right
        else if (horizontalVelocity > 0 && spriteRenderer.flipX)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Invert the local scale's X-axis to flip the sprite horizontally
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;

        // Update the flipX property of the SpriteRenderer
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
