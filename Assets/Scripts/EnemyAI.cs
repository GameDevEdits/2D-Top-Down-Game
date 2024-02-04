using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 100;
    public int currentHealth;

    public Transform spawnPoint;

    // List of spawnable items for health drops
    public List<SpawnableItem> healthDrops;

    private Transform player;
    private Animator anim;
    private bool isDead;

    public ScoreManager scoreManager;
    public PlayerTimer playerTimer;

    public TextMeshProUGUI scoreText;
    public float scoreIncrement = 10;
    public float killIncrement = 1;

    public ScoreScriptableObject scoreScriptableObject;

    public PommeBlock pommeBlock;
    public AudioSource chosenAudioSource;

    public float blockCooldown = 1f;
    private bool isBlockingOnCooldown = false;

    private bool canTakeDamage = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        StartCoroutine(DelayVulnerability());
    }


    private void Update()
    {
        if (!isDead && canTakeDamage)
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
        if (!isDead && canTakeDamage && (pommeBlock == null || !pommeBlock.IsBlocking()))
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            audioSource.enabled = false;
            StartAudioAgain();
        }

        chosenAudioSource.enabled = true;

        anim.SetBool("isDead", true);

        if (playerTimer != null)
        {
            playerTimer.IncrementKillCount();
        }

        if (scoreManager != null)
        {
            scoreManager.HandleEnemyDeath(gameObject);
        }

        IncrementScore();
        IncrementKills();

        StartCoroutine(DelayedSortingOrderChange());

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

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

        // Spawn a health drop
        SpawnHealthDrop();
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

        if (scoreText != null)
        {
            if (int.TryParse(scoreText.text, out int currentScore))
            {
                currentScore += (int)scoreIncrement;
                scoreText.text = currentScore.ToString();
            }
        }
    }

    IEnumerator DelayedSortingOrderChange()
    {
        yield return null;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder -= 10;
        }
    }

    IEnumerator BlockCooldown()
    {
        isBlockingOnCooldown = true;
        yield return new WaitForSeconds(blockCooldown);
        isBlockingOnCooldown = false;
    }

    IEnumerator DelayVulnerability()
    {
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }

    // Method to spawn a health drop
    void SpawnHealthDrop()
    {
        if (healthDrops.Count > 0)
        {
            float totalSpawnChances = 0f;

            foreach (var item in healthDrops)
            {
                totalSpawnChances += item.spawnChance;
            }

            float randomValue = Random.Range(0f, 1f); // Generate a new random value between 0 and 1

            if (randomValue > totalSpawnChances)
            {
                // Nothing spawns if the random value is greater than the total spawn chances
                return;
            }

            foreach (var item in healthDrops)
            {
                if (randomValue <= item.spawnChance)
                {
                    // Spawn the item
                    Vector3 spawnPosition = (spawnPoint != null) ? spawnPoint.position : transform.position;
                    Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                    return; // Exit the method after spawning an item
                }

                randomValue -= item.spawnChance;
            }
        }
    }
}
