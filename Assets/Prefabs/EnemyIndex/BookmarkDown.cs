using UnityEngine;

public class BookmarkDown : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();

        // Ensure animator is not null
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }

    private void Update()
    {
        // Check for left mouse button click in 2D
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position in 2D
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // Check if the ray hits the collider of this GameObject
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Set the "markDown" parameter to true in the animator
                animator.SetBool("markDown", true);
            }
        }
    }
}
