using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    // The speed of the floating animation
    public float speed = 1.0f;

    // The amplitude of the floating motion
    public float amplitude = 0.5f;

    // Initial position of the sprite
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the sprite
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new Y position
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Set the new position
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
