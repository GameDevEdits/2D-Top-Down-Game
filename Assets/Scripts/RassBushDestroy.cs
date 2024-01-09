using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RassBushDestroy : MonoBehaviour
{
    public Animator animator;
    public bool bushDestroy = false;
    public int hitsToDestroy = 3; // Set the number of hits required to destroy the bush

    private int hitCounter = 0;

    private void Start()
    {
        if (animator == null)
        {
            // If the animator is not assigned, try to get it from the same GameObject
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "PlayerBullet" or "EnemyBullet" tag
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            hitCounter++;

            // Check if the required number of hits is reached
            if (hitCounter >= hitsToDestroy)
            {
                // Set "bushDestroy" to true in the animator
                if (animator != null)
                {
                    animator.SetBool("bushDestroy", true);
                }

                // Disable the collider of the GameObject
                Collider2D collider = GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = false;
                }

                // Disable all children of the GameObject
                DisableAllChildren();
            }
        }
    }

    private void DisableAllChildren()
    {
        // Get all child transforms of the GameObject
        Transform[] children = GetComponentsInChildren<Transform>();

        // Disable all children, excluding the GameObject itself
        foreach (Transform child in children)
        {
            if (child != transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
