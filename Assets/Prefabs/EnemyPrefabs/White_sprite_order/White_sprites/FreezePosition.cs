using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour
{
    private Rigidbody2D rb; // Assuming 2D Rigidbody, change to Rigidbody if 3D

    private void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }
    }

    // Animation event to freeze the position
    public void FreezeFrame()
    {
        if (rb != null)
        {
            // Freeze only the position, not the rotation
            rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        }
    }

    // Animation event to unfreeze the position
    public void UnfreezePosition()
    {
        if (rb != null)
        {
            // Unfreeze the position constraints while keeping the rotation unaffected
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        }
    }
}
