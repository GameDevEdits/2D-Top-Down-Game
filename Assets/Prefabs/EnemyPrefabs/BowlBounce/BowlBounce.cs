using UnityEngine;

public class BowlBounce : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Get the contact point of collision
            ContactPoint2D contactPoint = collision.GetContact(0);

            // Calculate the normal of the collision surface
            Vector2 wallNormal = contactPoint.normal;

            // Calculate the velocity vector of the projectile
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

            // Calculate the reflected velocity
            Vector2 reflectedVelocity = Vector2.Reflect(velocity, wallNormal);

            // Apply the reflected velocity to the projectile
            GetComponent<Rigidbody2D>().velocity = reflectedVelocity;
        }
    }
}
