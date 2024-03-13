using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleController : MonoBehaviour
{
    private Animator animator;
    private KeyCode lastKeyUp;
    private KeyCode secondLastKeyUp;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if any keys are currently pressed
        bool anyKeyIsPressed = Input.anyKey;

        // Check if any key is released
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            // Update key press history
            secondLastKeyUp = lastKeyUp;
            lastKeyUp = Input.GetKeyUp(KeyCode.A) ? KeyCode.A : Input.GetKeyUp(KeyCode.D) ? KeyCode.D : Input.GetKeyUp(KeyCode.W) ? KeyCode.W : KeyCode.S;

            // Check for specific key sequences
            if (lastKeyUp == KeyCode.W)
            {
                if (secondLastKeyUp == KeyCode.A)
                {
                    SetAnimatorState(false, false, true, false);
                }
                else if (secondLastKeyUp == KeyCode.D)
                {
                    SetAnimatorState(false, false, false, true);
                }
                else
                {
                    SetAnimatorState(false, false, true, false); // Added condition for W release
                }
            }
            else if (lastKeyUp == KeyCode.A)
            {
                SetAnimatorState(true, false, false, false);
            }
            else if (lastKeyUp == KeyCode.D)
            {
                SetAnimatorState(false, true, false, false);
            }
            else if (lastKeyUp == KeyCode.S)
            {
                SetAnimatorState(false, true, false, false); // Set rightIdle to true when S is released
            }
            else
            {
                // Any other keys are being pressed, set both to false
                SetAnimatorState(false, false, false, false);
            }
        }
    }


    private void SetAnimatorState(bool leftIdle, bool rightIdle, bool backIdleLeft, bool backIdleRight)
    {
        // Set animator parameters
        animator.SetBool("leftIdle", leftIdle);
        animator.SetBool("rightIdle", rightIdle);
        animator.SetBool("backIdleLeft", backIdleLeft);
        animator.SetBool("backIdleRight", backIdleRight);
    }
}
