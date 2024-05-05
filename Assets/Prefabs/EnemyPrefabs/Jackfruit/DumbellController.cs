using System.Collections;
using UnityEngine;

public class DumbellController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the prefab moves downwards
    public float impactDelay = 2f; // Delay before setting Impact parameter to true in seconds

    private Animator animator;
    private bool hasImpacted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
        else
        {
            // Start the coroutine to trigger impact after a delay
            StartCoroutine(ImpactDelayCoroutine());
        }
    }

    void Update()
    {
        if (!hasImpacted)
        {
            MoveDownwards();
        }
    }

    // Move the object downwards
    private void MoveDownwards()
    {
        // Continuously move the object downwards
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    // Coroutine to set "Impact" parameter after a delay
    private IEnumerator ImpactDelayCoroutine()
    {
        yield return new WaitForSeconds(impactDelay);

        // Set "Impact" parameter to true in the animator
        if (animator != null)
        {
            animator.SetBool("Impact", true);
        }
        else
        {
            Debug.LogWarning("Animator component not found!");
        }

        // Set flag indicating impact
        hasImpacted = true;
    }

    // Method to freeze the position of the dumbell
    public void FreezeDumbell()
    {
        // Freeze the position of the dumbell
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Debug.LogWarning("No Rigidbody2D found on the dumbell. Position not frozen.");
        }
    }

    public void Impact()
    {
        // Set flag indicating impact
        hasImpacted = true;

        // Trigger any impact-related effects or animations here

        // Change the sprite order layer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 210;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found!");
        }
    }
}