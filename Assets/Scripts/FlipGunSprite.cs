using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGunSprite : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Get the mouse position in world coordinates.
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set the z-coordinate to zero (2D game).

        // Calculate the direction from the gun to the mouse cursor.
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Check if the gun should be flipped (facing left).
        if (direction.x < 0)
        {
            // Flip the gun sprite horizontally by changing its local scale on the x-axis.
            transform.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            // Restore the gun's scale when facing right.
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
