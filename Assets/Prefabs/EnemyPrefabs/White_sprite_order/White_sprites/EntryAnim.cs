using System.Collections;
using UnityEngine;

public class EntryAnim : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Start the coroutine to delay the entry animation
        StartCoroutine(DelayEntryAnimation());
    }

    IEnumerator DelayEntryAnimation()
    {
        // Disable the Animator to stop playing the entry animation
        animator.enabled = false;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Enable the Animator to resume animation playback
        animator.enabled = true;
    }
}
