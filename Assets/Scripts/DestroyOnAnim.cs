using UnityEngine;

public class DestroyOnAnim : MonoBehaviour
{
    // Function to be called by an animation event to destroy the GameObject
    public void DestroyGameObject()
    {
        // Start a coroutine to wait for 1 second before destroying the object
        StartCoroutine(DestroyWithDelay());
    }

    // Coroutine to destroy the GameObject after a delay
    private System.Collections.IEnumerator DestroyWithDelay()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
