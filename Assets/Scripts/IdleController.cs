using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if any keys are currently pressed
        bool anyKeyIsPressed = Input.anyKey;

        // Check for specific keys and their last key up
        if (Input.GetKeyUp(KeyCode.A) && !anyKeyIsPressed)
        {
            // Last key up is A and no other keys are being pressed
            SetAnimatorState(true, false);
        }
        else if (Input.GetKeyUp(KeyCode.D) && !anyKeyIsPressed)
        {
            // Last key up is D and no other keys are being pressed
            SetAnimatorState(false, true);
        }
        else if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) && !anyKeyIsPressed)
        {
            // Last key up is W or S and no other keys are being pressed
            SetAnimatorState(false, false);
        }
        else
        {
            // Any other keys are being pressed, set both to false
            SetAnimatorState(false, false);
        }
    }

    private void SetAnimatorState(bool leftIdle, bool rightIdle)
    {
        // Set animator parameters
        animator.SetBool("leftIdle", leftIdle);
        animator.SetBool("rightIdle", rightIdle);
    }
}
