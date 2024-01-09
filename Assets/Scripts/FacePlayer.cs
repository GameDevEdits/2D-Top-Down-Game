using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform target; // Drag and drop the player's GameObject in the Unity Inspector.

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction vector from this object to the target (player).
            Vector3 direction = target.position - transform.position;

            // Ensure the object faces the target without tilting up or down.
            direction.y = 0f;

            // Rotate the object to face the player.
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}