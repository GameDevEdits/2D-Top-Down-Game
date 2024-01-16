using System.Collections;
using UnityEngine;

public class PMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashMultiplier = 2f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float interactRadius = 3f; // Adjust the radius in the inspector
    public LayerMask chestLayer; // Set this in the inspector to the layer where your chests are

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForChest();
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

    void CheckForChest()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius, chestLayer);

        if (colliders.Length > 0)
        {
            GameObject nearestChest = GetNearestChest(colliders);

            // Assuming ChestController script is attached to the Chest GameObject
            ChestController chestController = nearestChest.GetComponent<ChestController>();

            if (chestController != null)
            {
                // Open the chest and trigger item spawning
                chestController.OpenChest();

                // Assuming you have an Animator component on the player GameObject
                animator.SetBool("chestOpening", true);

                StartCoroutine(ResetChestOpening());
            }
        }
    }

    GameObject GetNearestChest(Collider2D[] colliders)
    {
        GameObject nearestChest = colliders[0].gameObject;
        float nearestDistance = Vector2.Distance(transform.position, nearestChest.transform.position);

        for (int i = 1; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, colliders[i].gameObject.transform.position);

            if (distance < nearestDistance)
            {
                nearestChest = colliders[i].gameObject;
                nearestDistance = distance;
            }
        }

        return nearestChest;
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

    IEnumerator ResetChestOpening()
    {
        // Wait for the chest opening animation to finish
        yield return new WaitForSeconds(0.6f);

        // Reset the chestOpening boolean after the animation is done
        animator.SetBool("chestOpening", false);
    }
}
