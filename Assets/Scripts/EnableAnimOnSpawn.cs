using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimOnSpawn : MonoBehaviour
{
    private Animator animator;
    public GameObject spawnAnim;

    private void Start()
    {
        // Assuming the Animator component is attached to the same GameObject
        animator = GetComponent<Animator>();

        // Disable the Animator initially
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    private void SpawnObject()
    {
        // Instantiate your GameObject
        GameObject spawnedObject = Instantiate(spawnAnim, transform.position, transform.rotation);

        // Enable the Animator component on the spawned object
        if (spawnedObject != null)
        {
            Animator spawnedAnimator = spawnedObject.GetComponent<Animator>();

            if (spawnedAnimator != null)
            {
                spawnedAnimator.enabled = true;
            }
            else
            {
                Debug.LogWarning("Animator component not found on the spawned object.");
            }
        }
    }
}
