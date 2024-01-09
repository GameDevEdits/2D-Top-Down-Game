using UnityEngine;

public class PommeBlock : MonoBehaviour
{
    private Animator animator;
    private bool isBlocking;
    private Rigidbody2D rb;
    private bool isFrozen; // Flag to track if the position is frozen

    public float blockRadius = 3f;

    private void Start()
    {
        // Get the Animator component from the same GameObject
        animator = GetComponent<Animator>();
        // Get the Rigidbody2D component from the same GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a green wire sphere in the editor to visualize the block radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, blockRadius);
    }

    public void TriggerBlock(Transform playerTransform)
    {
        if (!isBlocking)
        {
            // Check if the player is within the block radius
            if (Vector2.Distance(playerTransform.position, transform.position) <= blockRadius)
            {
                isBlocking = true;

                if (animator != null)
                {
                    // Set "Block" to true
                    animator.SetBool("Block", true);
                }

                // Start a coroutine to set "Block" back to false after 1 second
                StartCoroutine(ResetBlockAfterDelay());
            }
        }
    }

    private System.Collections.IEnumerator ResetBlockAfterDelay()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Set "Block" back to false
        if (animator != null)
        {
            animator.SetBool("Block", false);
        }

        isBlocking = false;

        // Unfreeze the position using an animation event if it was frozen
        if (isFrozen && rb != null)
        {
            // Trigger an animation event to unfreeze the position
            rb.gameObject.SendMessage("UnfreezePosition", SendMessageOptions.DontRequireReceiver);
            isFrozen = false;
        }
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    // Animation event method to freeze the position
    public void FreezePosition()
    {
        FlipEnemyAI myScriptComponentFive = GetComponent<FlipEnemyAI>();
        if (myScriptComponentFive != null)
        {
            myScriptComponentFive.enabled = false;
        }

        if (!isFrozen && rb != null)
        {
            // Freeze the position by setting constraints
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFrozen = true;
        }
    }

    // Animation event method to unfreeze only the position
    public void UnfreezePosition()
    {
        FlipEnemyAI myScriptComponentFive = GetComponent<FlipEnemyAI>();
        if (myScriptComponentFive != null)
        {
            myScriptComponentFive.enabled = true;
        }

        if (isFrozen && rb != null)
        {
            // Unfreeze only the position, keep rotation frozen
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            isFrozen = false;
        }
    }
}
