using UnityEngine;

public class BulletLookAtPlayer : MonoBehaviour
{
    private Transform target;

    // Set the target when the bullet is instantiated
    public void Initialize(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        if (target != null)
        {
            // Calculate the direction from the bullet to the player
            Vector3 directionToPlayer = target.position - transform.position;

            // Set the bullet's rotation to face the player
            SetRotation(directionToPlayer.normalized);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Face the direction the bullet is moving
        SetRotation(GetComponent<Rigidbody2D>().velocity.normalized);
    }

    private void SetRotation(Vector3 direction)
    {
        // Calculate the rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation explicitly to avoid x and y rotation
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
