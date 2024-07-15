using System.Collections;
using UnityEngine;

public class PommeFlip : MonoBehaviour
{
    public bool flip;

    private Transform playerTransform;
    private bool canFlip = false;
    private bool initialFacingSet = false; // Flag to track if initial facing has been set
    private float delayTimer = 2f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(EnableFlipping());
    }

    private IEnumerator EnableFlipping()
    {
        yield return new WaitForSeconds(delayTimer);
        canFlip = true;

        // Set initial facing direction when flipping is enabled
        SetInitialFacingDirection();
    }

    // Method to set the initial facing direction towards the player
    private void SetInitialFacingDirection()
    {
        Vector3 scale = transform.localScale;

        if (playerTransform.position.x < transform.position.x)
        {
            // Player is on the left, flip the sprite
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        // If player is on the right, do nothing (leave initial facing as is)

        transform.localScale = scale;
        initialFacingSet = true; // Mark initial facing as set
    }

    // Method to handle flipping during animation events
    public void PommeFlipTurn()
    {
        if (!canFlip)
            return;

        // Only flip if initial facing has been set
        if (initialFacingSet)
        {
            Vector3 scale = transform.localScale;

            if (playerTransform.position.x > transform.position.x)
            {
                // Player is on the right, flip the sprite
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }
            else
            {
                // Player is on the left, flip the sprite back if necessary
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }

            transform.localScale = scale;
        }
    }
}
