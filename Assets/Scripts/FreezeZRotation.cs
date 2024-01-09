using UnityEngine;

public class FreezeZRotation : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody component from the same GameObject
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found. Make sure this script is attached to a GameObject with a Rigidbody.");
        }
    }

    void Update()
    {
        // Ensure that the Rigidbody is not null
        if (rb != null)
        {
            // Freeze the z-rotation using constraints
            rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
