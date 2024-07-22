using System.Collections;
using UnityEngine;

public class DumbellController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the prefab moves downwards
    public float impactDelay = 2f; // Delay before setting Impact parameter to true in seconds
    public GameObject shadowPrefab; // Reference to the shadow prefab

    private Animator animator;
    private bool hasImpacted = false;
    private GameObject shadowInstance;

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
        // Wait for half of the impact delay
        yield return new WaitForSeconds(impactDelay / 2);

        // Calculate the future position where the dumbbell will impact
        Vector3 futurePosition = transform.position + Vector3.down * moveSpeed * (impactDelay / 2);

        // Instantiate the shadow prefab at the predicted impact position
        if (shadowPrefab != null)
        {
            shadowInstance = Instantiate(shadowPrefab, futurePosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Shadow prefab is not assigned!");
        }

        // Wait for the remaining time of the impact delay
        yield return new WaitForSeconds(impactDelay / 2);

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

    // Method to freeze the position of the dumbbell
    public void FreezeDumbbell()
    {
        // Freeze the position of the dumbbell
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Debug.LogWarning("No Rigidbody2D found on the dumbbell. Position not frozen.");
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

        // Destroy the shadow instance after the impact
        if (shadowInstance != null)
        {
            Destroy(shadowInstance);
        }
    }
}
