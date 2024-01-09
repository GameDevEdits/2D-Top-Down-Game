using System.Collections;
using UnityEngine;

public class PMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashMultiplier = 2f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;

    private float originalMoveSpeed;
    private float dashTimer;
    private bool isDashing;
    private float dashCooldownTimer;

    public Rigidbody2D rb;
    public Animator animator;
    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;

    private Vector2 rawInput;
    private Vector2 movement;

    private bool isIdle = true;

    void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        rawInput.x = Input.GetAxisRaw("Horizontal");
        rawInput.y = Input.GetAxisRaw("Vertical");

        movement = rawInput.normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (isIdle)
        {
            UpdateIdleDirection();
        }

        // Check if the player is not blocking and the dash conditions are met
        if (!GetComponent<PlayerHealth>().IsBlocking() && Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldownTimer <= 0f)
        {
            StartDash();
        }

        if (movement.sqrMagnitude > 1f)
        {
            PlayFootstepAudio();
        }
        else
        {
            StopFootstepAudio();
        }

        if (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        isIdle = Mathf.Approximately(movement.sqrMagnitude, 0f);
    }

    void StartDash()
    {
        // Check if the player is not blocking
        if (!GetComponent<PlayerHealth>().IsBlocking())
        {
            moveSpeed *= dashMultiplier;
            dashTimer = dashDuration;
            isDashing = true;
        }
    }

    void EndDash()
    {
        moveSpeed = originalMoveSpeed;
        isDashing = false;
        dashCooldownTimer = dashCooldown;
    }

    private void UpdateIdleDirection()
    {
        if (movement.x > 0)
        {
            animator.SetFloat("IdleDirectionX", 1f);
        }
        else if (movement.x < 0)
        {
            animator.SetFloat("IdleDirectionX", -1f);
        }
        else
        {
            animator.SetFloat("IdleDirectionX", 0f);
        }
    }

    void PlayFootstepAudio()
    {
        if (footstepAudioSource != null && footstepSound != null)
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = footstepSound;
                footstepAudioSource.loop = true;
                footstepAudioSource.Play();
            }
        }
    }

    void StopFootstepAudio()
    {
        if (footstepAudioSource != null && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }
    }

    public void FreezePosition()
    {
        if (rb != null)
        {
            // Freeze only the position by setting constraints
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    // Animation event method to unfreeze the position
    public void UnfreezePosition()
    {
        if (rb != null)
        {
            // Unfreeze only the position by clearing position constraints
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
        }
    }
}
