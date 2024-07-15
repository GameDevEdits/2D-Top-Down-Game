using System.Collections;
using UnityEngine;

public class PommeTurn : MonoBehaviour
{
    private Animator animator;
    private Transform playerTransform;
    private bool isTurning;
    private bool faceRight; // Current facing direction
    private bool faceLeft;  // Current facing direction

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialize facing direction based on player's initial position
        UpdateFacingDirection(playerTransform.position.x > transform.position.x);
    }

    private void Update()
    {
        // Check if the player is on the left or right of the enemy
        bool playerIsRight = playerTransform.position.x > transform.position.x;

        // Check if the current facing direction matches the player's position
        if (playerIsRight && !faceRight)
        {
            // Player is on the right, but enemy is facing left
            TurnTowardsPlayer();
        }
        else if (!playerIsRight && !faceLeft)
        {
            // Player is on the left, but enemy is facing right
            TurnTowardsPlayer();
        }
    }

    private void TurnTowardsPlayer()
    {
        if (!isTurning)
        {
            isTurning = true;
            animator.SetBool("isTurn", true);
        }
    }

    // Animation event method to reset the isTurn parameter
    public void TurnFalse()
    {
        animator.SetBool("isTurn", false);
        isTurning = false;
        // Update facing direction after turning
        UpdateFacingDirection(playerTransform.position.x > transform.position.x);
    }

    // Method to update facing direction based on player's position
    private void UpdateFacingDirection(bool isPlayerRight)
    {
        faceRight = isPlayerRight;
        faceLeft = !isPlayerRight;
    }
}
