using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public float rollForce = 10f;
    public float rollDuration = 0.5f;
    public float dashForce = 10f;
    public float rollCooldown = 2f; // Set the cooldown duration

    private bool isRolling = false;
    private Rigidbody2D rb;
    private Animator animator;
    private float rollCooldownTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRolling && rollCooldownTimer <= 0f)
        {
            StartCoroutine(PerformDodgeRoll());
            Dash();
        }

        // Update roll cooldown timer
        if (rollCooldownTimer > 0f)
        {
            rollCooldownTimer -= Time.deltaTime;
        }
    }

    IEnumerator PerformDodgeRoll()
    {
        isRolling = true;

        // Save the current player velocity
        Vector2 savedVelocity = rb.velocity;

        // Apply a force to the player for the dash effect
        rb.velocity = transform.up * rollForce;

        // Play the dodge roll animation
        animator.SetBool("isRolling", true);

        // Wait for the specified duration
        yield return new WaitForSeconds(rollDuration);

        // Reset velocity after the roll is complete
        rb.velocity = savedVelocity;

        // Stop the dodge roll animation
        animator.SetBool("isRolling", false);

        isRolling = false;

        // Set the roll cooldown timer
        rollCooldownTimer = rollCooldown;
    }

    void Dash()
    {
        // Apply a force to the player for the dash effect
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        rb.velocity = dashDirection * dashForce;
    }
}
