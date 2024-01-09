using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAlwaysLookAtPlayer : MonoBehaviour
{
    private Transform target;

    // Set the target when the bullet is instantiated
    public void Initialize(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (target != null)
        {
            // Calculate the direction from the bullet to the player
            Vector3 directionToPlayer = target.position - transform.position;

            // Set the bullet's rotation to face the player
            transform.right = directionToPlayer.normalized;
        }
    }
}
