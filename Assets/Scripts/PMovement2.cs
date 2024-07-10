using System.Collections;
using UnityEngine;

public class PMovement2 : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float dashMultiplier = 2f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float interactRadius = 3f; // Adjust the radius in the inspector
    public LayerMask chestLayer; // Set this in the inspector to the layer where your chests are
    public float playerDashCooldown = 3f; // Cooldown time for playerDash

    private float originalMoveSpeed;
    private float dashTimer;
    private bool isDashing;
    private float dashCooldownTimer;
    private float playerDashCooldownTimer; // Timer for playerDash cooldown

    public Rigidbody2D rb;
    public Animator animator;
    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;

    private Vector2 rawInput;
    private Vector2 movement;
    private Vector2 lastDirection;

    private bool isIdle = true;
    public bool playerDash = false; // Track the second dash state
    private bool isSlowed = false; // Track if player is slowed down

    void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // Return early if playerDash or isRolling is true to ignore other inputs
        if (playerDash || animator.GetBool("isRolling"))
        {
            movement = lastDirection;
            moveSpeed = 15f; // Ensure speed is set correctly during dash or roll
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            return;
        }

        rawInput.x = Input.GetAxisRaw("Horizontal");
        rawInput.y = Input.GetAxisRaw("Vertical");

        if (rawInput != Vector2.zero)
        {
            lastDirection = rawInput.normalized;
        }

        movement = rawInput.normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Check if the player is not blocking and the dash conditions are met
        if (!GetComponent<PlayerHealth>().IsBlocking() && Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldownTimer <= 0f)
        {
            StartDash();
        }

        // Check for the second dash only if not rolling
        if (!animator.GetBool("isRolling") && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && playerDashCooldownTimer <= 0f)
        {
            StartSecondDash();
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

        if (playerDashCooldownTimer > 0f)
        {
            playerDashCooldownTimer -= Time.deltaTime;
        }

        // Set animator parameters for back idle
        bool isWKeyUp = Input.GetKeyUp(KeyCode.W);
        bool isAKeyUp = Input.GetKeyUp(KeyCode.A);
        bool isDKeyUp = Input.GetKeyUp(KeyCode.D);
        bool isSKeyUp = Input.GetKeyUp(KeyCode.S);

        animator.SetBool("backIdleLeft", isWKeyUp && (isAKeyUp || Input.GetKeyUp(KeyCode.S)));
        animator.SetBool("backIdleRight", isWKeyUp && isDKeyUp);
        animator.SetBool("rightIdle", isSKeyUp);

        // Check if the player is idle
        isIdle = Mathf.Approximately(movement.sqrMagnitude, 0f);

        // Adjust player speed based on dashing, rolling, and slowed states
        if (animator.GetBool("isRolling"))
        {
            moveSpeed = 15f;
        }
        else if (playerDash)
        {
            moveSpeed = 15f;
        }
        else if (isSlowed)
        {
            moveSpeed = 3f;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void StartDash()
    {
        // Check if the player is not blocking
        if (!GetComponent<PlayerHealth>().IsBlocking())
        {
            moveSpeed = 15f; // Set the speed to 15 for rolling
            dashTimer = dashDuration;
            isDashing = true;
            animator.SetBool("isRolling", true);
        }
    }

    void EndDash()
    {
        moveSpeed = originalMoveSpeed;
        isDashing = false;
        dashCooldownTimer = dashCooldown;
        animator.SetBool("isRolling", false);
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

    public void SlowM()
    {
        isSlowed = true; // Set flag for slowed movement
        moveSpeed = 3f;
    }

    public void NormalM()
    {
        isSlowed = false; // Clear flag for slowed movement
        moveSpeed = originalMoveSpeed;
    }

    void StartSecondDash()
    {
        // Check if not rolling
        if (!animator.GetBool("isRolling"))
        {
            playerDash = true;
            animator.SetBool("playerDash", true);
            moveSpeed = 15f;
            playerDashCooldownTimer = playerDashCooldown; // Start the cooldown timer
        }
    }

    // This method will be called by an animation event at the end of the dash animation
    public void EndSecondDash()
    {
        playerDash = false;
        animator.SetBool("playerDash", false);
        moveSpeed = originalMoveSpeed;
    }
}
