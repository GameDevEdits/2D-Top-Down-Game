using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered the trigger has the tag "Player".
        if (other.CompareTag("Player"))
        {
            // Destroy the SShiv object.
            Destroy(gameObject);
        }
    }
}
