using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }

    // Method to be called from an animation event
    public void DestroyGameObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
