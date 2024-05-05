using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 3f; // Initial speed
    public float maxSpeed = 7f; // Maximum speed
    public float rotationSpeed = 180f; // Adjust this value for the desired rotation speed
    private float timeElapsed = 0f; // Time elapsed since the bullet's creation

    public void Start()
    {
        // Find the player dynamically at runtime
        FindPlayer();
    }

    private void Update()
    {
        // Increment timeElapsed
        timeElapsed += Time.deltaTime;

        // Increase moveSpeed gradually up to maxSpeed
        moveSpeed = Mathf.Min(moveSpeed + (0.5f * Time.deltaTime), maxSpeed);

        // Check if the player is still valid
        if (player != null)
        {
            // Calculate the direction to the player
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            // Rotate towards the player
            RotateTowardsPlayer(direction);

            // Move towards the player
            MoveCharacter(direction);
        }
    }

    private void RotateTowardsPlayer(Vector2 direction)
    {
        // Calculate the rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation explicitly to avoid x and y rotation
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void MoveCharacter(Vector2 direction)
    {
        // Move the character based on the direction and moveSpeed
        GetComponent<Rigidbody2D>().velocity = direction * moveSpeed;
    }

    private void FindPlayer()
    {
        // Attempt to find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Check if the player GameObject was found
        if (playerObject != null)
        {
            // Get the Transform component of the player
            player = playerObject.transform;
        }
        else
        {
            // Log a warning if the player is not found
            Debug.LogWarning("Player not found. Ensure the player has the 'Player' tag.");
        }
    }
}
