using UnityEngine;

public class BowlBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get the current speed of the ball
        float speed = lastVelocity.magnitude;

        // Determine the direction of collision
        Vector2 direction = other.ClosestPoint(transform.position) - (Vector2)transform.position;

        // Flip the velocity components based on collision direction
        Vector2 newVelocity = new Vector2(
            Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? -lastVelocity.x : lastVelocity.x,
            Mathf.Abs(direction.y) > Mathf.Abs(direction.x) ? -lastVelocity.y : lastVelocity.y
        );

        // Apply the new velocity while maintaining the speed
        rb.velocity = newVelocity.normalized * speed;
    
    }
}
