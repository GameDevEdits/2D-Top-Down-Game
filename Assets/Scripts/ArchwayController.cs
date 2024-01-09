using UnityEngine;

public class ArchwayController : MonoBehaviour
{
    public Animator animator;
    public string playerTag = "Player";
    public float activationRadius = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering object has the specified tag ("Player")
        if (other.CompareTag(playerTag))
        {
            // Set "openArchway" in the animator to true
            if (animator != null)
            {
                animator.SetBool("openArchway", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object has the specified tag ("Player")
        if (other.CompareTag(playerTag))
        {
            // Set "openArchway" in the animator to false
            if (animator != null)
            {
                animator.SetBool("openArchway", false);
            }
        }
    }
}
