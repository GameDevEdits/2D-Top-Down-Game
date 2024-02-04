using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOpen : MonoBehaviour
{
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDisable;
    public float fadeInDuration = 1f;
    public float delayDuration = 1f;

    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        // Get the Animator component attached to the same game object
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position in 2D space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hits a collider
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Toggle the book state
                isOpen = !isOpen;

                // Set "openBook" to the book state in the Animator
                if (animator != null)
                {
                    animator.SetBool("openBook", isOpen);
                }

                // Enable or disable objects based on the book state with a delay
                StartCoroutine(isOpen ? EnableObjectsWithDelay() : DisableObjectsWithDelay());
            }
        }
    }

    // Coroutine to enable specified game objects with fade-in effect after a delay
    private IEnumerator EnableObjectsWithDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        StartCoroutine(FadeInObjects());
    }

    // Coroutine to disable specified game objects after a delay
    private IEnumerator DisableObjectsWithDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        DisableObjects();
    }

    // Method to enable specified game objects with fade-in effect
    private void EnableObjects()
    {
        StartCoroutine(FadeInObjects());
    }

    // Method to disable specified game objects
    private void DisableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
    }

    // Coroutine for fade-in effect
    private IEnumerator FadeInObjects()
    {
        // Create a new CanvasGroup to handle the fade for all objects
        CanvasGroup group = gameObject.AddComponent<CanvasGroup>();

        // Enable the objects
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        // Gradually increase alpha for fade-in effect
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            group.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha is set to 1 at the end
        group.alpha = 1f;

        // Remove the CanvasGroup component when the fade is complete
        Destroy(group);

        // Disable specified game objects after a delay
        yield return new WaitForSeconds(delayDuration);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
