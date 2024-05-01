using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFlipPage : MonoBehaviour
{
    public Collider2D bookCollider;
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDisable;
    public float fadeInDuration = 1f;
    public float delayDuration = 1f;
    public Animator animator;
    public Animator otherAnimator; // New public variable for the other Animator

    private bool pageFlipped = false;

    // Ensure the script is disabled by default
    private void Start()
    {
        enabled = false;
    }

    private void Update()
    {
        // Check if the book is open and page flipping is allowed
        if (enabled && Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position in 2D space
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hits the specified collider
            if (hit.collider != null && hit.collider == bookCollider)
            {
                if (!pageFlipped)
                {
                    // Immediately disable objects on the first click
                    foreach (GameObject obj in objectsToDisable)
                    {
                        obj.SetActive(false);
                    }

                    // Start coroutine to delay setting "markDown" to true in the other animator
                    StartCoroutine(DelayMarkDown());

                    PageFlip();
                }
                else
                {
                    // Check if objects to enable are already active
                    bool objectsActive = true;
                    foreach (GameObject obj in objectsToEnable)
                    {
                        if (!obj.activeSelf)
                        {
                            objectsActive = false;
                            break;
                        }
                    }

                    // If objects are already active, return without doing anything
                    if (objectsActive)
                        return;

                    ResetPage();
                }
            }
        }
    }

    private IEnumerator DelayMarkDown()
    {
        yield return new WaitForSeconds(0.5f);

        // Set "markDown" to true in the other animator after the delay
        if (otherAnimator != null)
        {
            otherAnimator.SetBool("markDown", true);
        }
    }

    private void PageFlip()
    {
        // Set "flipPage" to true in the Animator
        if (animator != null)
        {
            animator.SetBool("flipPage", true);
        }

        // Enable or disable objects based on the delay
        StartCoroutine(EnableObjectsWithDelay());

        pageFlipped = true;
    }

    private void ResetPage()
    {
        // Set "flipPage" to false in the Animator after a delay
        if (animator != null)
        {
            animator.SetBool("flipPage", false);
        }

        // Disable objects without delay
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        pageFlipped = false;
    }

    // Coroutine to enable specified game objects with fade-in effect after a delay
    private IEnumerator EnableObjectsWithDelay()
    {
        yield return new WaitForSeconds(delayDuration);

        // Enable the objects
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        // Gradually increase alpha for fade-in effect
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
