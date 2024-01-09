using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnim : MonoBehaviour
{
    public Transform target;

    private void OnEnable()
    {
        // Assuming you have an "isSpawned" boolean in your Animator
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isSpawned", true);
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Set the position of this GameObject to match the target's position
            transform.position = target.position;
        }
    }
}