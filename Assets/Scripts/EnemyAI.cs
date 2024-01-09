using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this for TextMeshPro

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 100;
    public int currentHealth;

    private Transform player;
    private Animator anim;
    private bool isDead;

    public ScoreManager scoreManager;
    public PlayerTimer playerTimer;

    public TextMeshProUGUI scoreText;
    public float scoreIncrement = 10;
    public float killIncrement = 1;

    public ScoreScriptableObject scoreScriptableObject;

    public PommeBlock pommeBlock; // Drag and drop your PommeBlock script in the Inspector.
    public AudioSource chosenAudioSource; // Assign this in the Inspector.

    public float blockCooldown = 1f; // Add a public variable for the block cooldown

    private bool isBlockingOnCooldown = false; // Flag to track if blocking is on cooldown

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isDead)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);

            // Check for left-click
            if (Input.GetMouseButtonDown(0) && !isBlockingOnCooldown)
            {
                // Call the TriggerBlock method on PommeBlock if it's not null
                if (pommeBlock != null)
                {
                    pommeBlock.TriggerBlock(player);
                    // Start the block cooldown
                    StartCoroutine(BlockCooldown());
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead && (pommeBlock == null || !pommeBlock.IsBlocking()))
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


    // Function to handle the death of the enemy.
    public void Die()
    {
        isDead = true;  // Set the flag to indicate that the enemy is dead.

        // Disable all currently playing audio sources
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            audioSource.enabled = false;
            StartAudioAgain();
        }

        chosenAudioSource.enabled = true;


        // Trigger the "isDead" parameter in the Animator to play the die animation.
        anim.SetBool("isDead", true);

        if (playerTimer != null)
        {
            playerTimer.IncrementKillCount();
        }

        if (scoreManager != null)
        {
            scoreManager.HandleEnemyDeath(gameObject);
        }

        // Increment the score
        IncrementScore();
        IncrementKills();

        StartCoroutine(DelayedSortingOrderChange());

        // You may also want to disable any enemy components like movement scripts, shooting scripts, etc.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop rigidbody movement
            rb.gravityScale = 0f; // Disable gravity (if applicable)
            rb.isKinematic = true; // Make the rigidbody kinematic
        }

        // Disable the Collider2D component to prevent further interactions with the player.
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        //Disable children
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        // Disable the "MyScript" component.
        EnemyAI myScriptComponentOne = GetComponent<EnemyAI>();
        if (myScriptComponentOne != null)
        {
            myScriptComponentOne.enabled = false;
        }

        EnemyFlash myScriptComponentTwo = GetComponent<EnemyFlash>();
        if (myScriptComponentTwo != null)
        {
            myScriptComponentTwo.enabled = false;
        }

        NewEnemyMADShank myScriptComponentThree = GetComponent<NewEnemyMADShank>();
        if (myScriptComponentThree != null)
        {
            myScriptComponentThree.enabled = false;
        }

        EnemyMADShank myScriptComponentFour = GetComponent<EnemyMADShank>();
        if (myScriptComponentFour != null)
        {
            myScriptComponentFour.enabled = false;
        }

        FlipEnemyAI myScriptComponentFive = GetComponent<FlipEnemyAI>();
        if (myScriptComponentFive != null)
        {
            myScriptComponentFive.enabled = false;
        }

        NewMRAVisual myScriptComponentSix = GetComponent<NewMRAVisual>();
        if (myScriptComponentSix != null)
        {
            myScriptComponentSix.enabled = false;
        }

        NewEnemyMRA myScriptComponentSeven = GetComponent<NewEnemyMRA>();
        if (myScriptComponentSeven != null)
        {
            myScriptComponentSeven.enabled = false;
        }
        // Optionally, you can disable the collider to prevent further interactions with the player.
    }

    void StartAudioAgain()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            audioSource.enabled = true;
        }
    }

    void IncrementKills()
    {
        scoreScriptableObject.AddKill(killIncrement);
    }

    void IncrementScore()
    {
        scoreScriptableObject.AddScore(scoreIncrement);

        // Check if the scoreText is assigned
        if (scoreText != null)
        {
            // Parse the current score text
            if (int.TryParse(scoreText.text, out int currentScore))
            {
                // Increment the score
                currentScore += (int)scoreIncrement;

                // Update the score text
                scoreText.text = currentScore.ToString();
            }
        }
    }

    IEnumerator DelayedSortingOrderChange()
    {
        yield return null;  // Wait for the next frame.

        // Change sorting order.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder -= 10;
        }

        // Optionally, you can disable the collider to prevent further interactions with the player.
    }

    IEnumerator BlockCooldown()
    {
        isBlockingOnCooldown = true;
        yield return new WaitForSeconds(blockCooldown);
        isBlockingOnCooldown = false;
    }
}