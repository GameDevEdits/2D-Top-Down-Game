using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RassBushTopBottom : MonoBehaviour
{
    public Animator bush1Animator;
    public Animator bush2Animator;

    private void Start()
    {
        // Ensure that the animators are assigned in the Inspector
        if (bush1Animator == null || bush2Animator == null)
        {
            Debug.LogError("Please assign the animators in the Inspector.");
        }
    }

    private void Update()
    {
        // Check if bush 2's "bushDestroy" becomes true
        if (bush2Animator.GetBool("bushDestroy"))
        {
            // Set bush 1's "bushDestroy" to true
            bush1Animator.SetBool("bushDestroy", true);
        }

        if (bush2Animator.GetBool("bushAsh"))
        {
            // Set bush 1's "bushDestroy" to true
            bush1Animator.SetBool("bushAsh", true);
        }
    }
}