using UnityEngine;

public class BulletRotation : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public Transform bulletPrefab;
    public Transform spawnPoint;
    public Transform player;

    private Vector3 initialDirection;

    // Set the initial direction when the bullet is instantiated
    public void Initialize(Vector3 initialDirection)
    {
        this.initialDirection = initialDirection.normalized;
        transform.right = this.initialDirection; // Set initial rotation
    }

    void Update()
    {
        // Rotate towards the initial direction over time
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, initialDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
