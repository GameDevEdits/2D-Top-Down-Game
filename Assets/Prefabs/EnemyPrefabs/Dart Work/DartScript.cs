using UnityEngine;

public class DartScript : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    private bool isFrozen = false; // Flag to track if the dart is frozen


    void Update()
    {
        // Move the dart only if it is not frozen
        if (!isFrozen)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void Initialize(Vector2 dir, float dartSpeed)
    {
        direction = dir;
        speed = dartSpeed;
    }

    // This method is called by an animation event to freeze the dart's movement and reset rotation
    public void FreezeDart()
    {
        // Freeze the dart's position
        isFrozen = true;

        // Reset the dart's rotation
        transform.rotation = Quaternion.identity; // Set to no rotation
    }
}
