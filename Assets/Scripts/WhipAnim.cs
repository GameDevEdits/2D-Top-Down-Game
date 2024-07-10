using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipAnim : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void AdjustAnimationSpeed(float newFireRate)
    {
        // Assume the base fire rate is 0.5 (can be adjusted according to your game's initial fire rate)
        float baseFireRate = 0.5f;

        // Calculate the speed factor based on the fire rate increase
        float speedFactor = newFireRate / baseFireRate;

        // Adjust the animation speed
        animator.speed = speedFactor;
    }
}
