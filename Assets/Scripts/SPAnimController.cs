using UnityEngine;

public class SPAnimController : MonoBehaviour
{
    public Animator animator;
    public float detectionRadius = 12.0f; // Adjust this value for the desired radius
    public LayerMask playerLayer;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found!");
            }
        }
    }

    private void Update()
    {
        // Check if any object with the tag "Player" is within the specified radius
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        // Set the "playSpawn" parameter in the Animator based on player detection
        if (playerCollider != null)
        {
            animator.SetBool("playSpawn", true);
        }
        else
        {
            animator.SetBool("playSpawn", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the Scene view to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
