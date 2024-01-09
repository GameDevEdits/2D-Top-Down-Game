using UnityEngine;

public class BFaceP : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Replace "Player" with the tag of your player object
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the bullet to the player
            Vector3 direction = player.position - transform.position;

            // Calculate the angle to rotate the bullet's sprite towards the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the rotation to the bullet's sprite
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}