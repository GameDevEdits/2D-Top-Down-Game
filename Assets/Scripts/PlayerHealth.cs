using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isDead = false;
    private Animator animator;

    public float blockCooldown = 2.0f;  // Set your desired cooldown time
    private float blockCooldownTimer = 0.0f;
    private bool isBlocking = false;

    private bool canTakeDamage = true;

    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    public float desaturationDuration = 3.0f;

    public GameObject enemySpawner; // Reference to the EnemySpawner GameObject

    private List<GameObject> uiElements = new List<GameObject>(); // List to store UI elements

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();

        if (globalVolume != null && globalVolume.profile.TryGet(out colorAdjustments))
        {
            // Disable desaturation at the start
            colorAdjustments.saturation.value = 100.0f;
        }
        else
        {
            Debug.LogError("Volume or ColorAdjustments settings not found on the 'Global Volume' GameObject.");
        }

        // Initialize cooldown timer
        blockCooldownTimer = 0.0f;

        // Find and store all UI elements in the scene
        FindAndStoreUIElements();
    }

    private void Update()
    {
        // Update cooldown timer
        blockCooldownTimer -= Time.deltaTime;

        // Check for user input to block
        if (Input.GetMouseButtonDown(1) && blockCooldownTimer <= 0.0f)
        {
            // Check if the player is not currently rolling
            if (!animator.GetBool("isRolling"))
            {
                StartBlocking();
            }
        }
    }

    private void FindAndStoreUIElements()
    {
        // Find all game objects with the tag "UI"
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");

        // Add UI elements to the list
        foreach (GameObject uiObject in uiObjects)
        {
            uiElements.Add(uiObject);
        }
    }

    private void DisableUI()
    {
        // Disable all UI elements in the list
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }

    // Method to take damage and update health
    public void TakeDamage(int damage)
    {
        if (!isDead && currentHealth > 0 && canTakeDamage)
        {
            currentHealth -= damage;

            // Check if the player has been defeated
            if (currentHealth <= 0)
            {
                Die();
                FreezeAllEnemies();
                StartCoroutine(DesaturateSceneCoroutine());

                // Disable the enemy spawner
                if (enemySpawner != null)
                {
                    enemySpawner.SetActive(false);
                }
            }
        }
    }

    // Method to gain health
    public void GainHealth(int amount)
    {
        // Increment the current health by the specified amount
        currentHealth += amount;

        // Clamp the current health to not exceed the maximum health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Method to handle player's death
    private void Die()
    {
        if (!isDead)
        {
            // Set the flag to indicate that the player is dead
            isDead = true;

            // Set the "isDead" parameter in the Animator to true to play the death animation
            animator.SetBool("isDead", true);

            // Optionally, you can add a delay before reloading the scene or other game over actions
            Invoke("ReloadScene", 6.0f);

            // Disable player controls and interactions
            DisablePlayer();

            // Disable all UI elements
            DisableUI();
        }
    }

    void DisablePlayer()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop rigidbody movement
            rb.gravityScale = 0f; // Disable gravity (if applicable)
            rb.isKinematic = true; // Make the rigidbody kinematic
        }

        PMovement2 myScriptComponentOne = GetComponent<PMovement2>();
        if (myScriptComponentOne != null)
        {
            myScriptComponentOne.enabled = false;
        }

        Shooting myScriptComponentTwo = GetComponent<Shooting>();
        if (myScriptComponentTwo != null)
        {
            myScriptComponentTwo.enabled = false;
        }

        AimMouseWeapon myScriptComponentThree = GetComponent<AimMouseWeapon>();
        if (myScriptComponentThree != null)
        {
            myScriptComponentThree.enabled = false;
        }

        DisableAllChildren();
    }

    // Coroutine to gradually desaturate the scene colors
    private System.Collections.IEnumerator DesaturateSceneCoroutine()
    {
        float startTime = Time.time;

        while (Time.time < startTime + desaturationDuration && colorAdjustments != null)
        {
            float t = (Time.time - startTime) / desaturationDuration;

            // Linearly interpolate between the current saturation and -100 (completely desaturated)
            colorAdjustments.saturation.value = Mathf.Lerp(0.0f, -100.0f, t);

            yield return null;
        }
    }

    void FreezeAllEnemies()
    {
        // Find all game objects with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Iterate through each enemy and deactivate all relevant components
        foreach (GameObject enemy in enemies)
        {
            // Disable the entire GameObject
            enemy.SetActive(false);
        }
    }

    private void DisableAllChildren()
    {
        // Get all child transforms of the player
        Transform[] children = GetComponentsInChildren<Transform>();

        // Disable all children, excluding the player's own transform
        foreach (Transform child in children)
        {
            if (child != transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartBlocking()
    {
        if (!isBlocking && blockCooldownTimer <= 0.0f)
        {
            // Set the flag to indicate that the player is blocking
            isBlocking = true;

            // Set the "isBlocking" parameter in the Animator to true to play the blocking animation
            animator.SetBool("isBlocking", true);

            // Optionally, you can add a delay before blocking ends or other actions
            // Invoke("StopBlocking", 2.0f);

            // Start cooldown timer
            blockCooldownTimer = blockCooldown;

            // Stop taking damage during blocking
            canTakeDamage = false;

            // Freeze the player's position during blocking
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }

            // Trigger animation event to stop damage
            animator.SetTrigger("StopDamage");
        }
    }

    // Animation event method to resume taking damage
    public void ResumeDamage()
    {
        // Resume taking damage after blocking animation finishes
        canTakeDamage = true;

        // Trigger animation event to resume damage
        animator.SetTrigger("ResumeDamage");

        isBlocking = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        animator.SetBool("isBlocking", false);
    }

    // Inside the PlayerHealth class
    public bool IsBlocking()
    {
        return isBlocking;
    }

    // Inside the PlayerHealth class
    public void RollHealthPause()
    {
        // Stop taking damage during rolling
        canTakeDamage = false;
    }

    // Inside the PlayerHealth class
    public void RollHealthResume()
    {
        // Resume taking damage after rolling animation finishes
        canTakeDamage = true;
    }
}
