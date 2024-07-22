using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isDead = false;
    private Animator animator;

    public float blockCooldown = 2.0f;
    private float blockCooldownTimer = 0.0f;
    private bool isBlocking = false;

    private bool canTakeDamage = true;
    private bool hasTakenDamage = false;

    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    public float desaturationDuration = 3.0f;

    public GameObject enemySpawner;

    private List<GameObject> uiElements = new List<GameObject>();

    private PMovement2 playerMovementScript;

    public TMP_Text invincibilityText;

    private bool isInvincible = false;
    private bool isRevived = false;

    private int revivalChance = 0; // Initial chance of revival

    // New public field for the icon
    public GameObject healthIcon;
    private Animator healthIconAnimator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();

        if (globalVolume != null && globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 100.0f;
        }
        else
        {
            Debug.LogError("Volume or ColorAdjustments settings not found on the 'Global Volume' GameObject.");
        }

        blockCooldownTimer = 0.0f;
        FindAndStoreUIElements();
        playerMovementScript = GetComponent<PMovement2>();

        if (invincibilityText != null)
        {
            invincibilityText.gameObject.SetActive(false);
        }

        // Initialize the health icon animator
        if (healthIcon != null)
        {
            healthIconAnimator = healthIcon.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        blockCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInvincibility();
        }

        if (Input.GetMouseButtonDown(1) && blockCooldownTimer <= 0.0f)
        {
            if (!animator.GetBool("isRolling") && (playerMovementScript == null || !playerMovementScript.playerDash))
            {
                StartBlocking();
            }
        }

        // Update health icon based on current health
        UpdateHealthIcon();
    }

    private void FindAndStoreUIElements()
    {
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");

        foreach (GameObject uiObject in uiObjects)
        {
            uiElements.Add(uiObject);
        }
    }

    private void DisableUI()
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead && currentHealth > 0 && canTakeDamage && !isInvincible && !isRevived)
        {
            int previousHealth = currentHealth;
            currentHealth -= damage;
            int newHealth = currentHealth;

            if (((previousHealth - 1) / 50 != (newHealth - 1) / 50) || ((previousHealth - 1) / 100 != (newHealth - 1) / 100))
            {
                StartCoroutine(StopTakingDamageForOneSecond());
            }

            hasTakenDamage = true;
            StartCoroutine(DamageCooldownCoroutine());

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator DamageCooldownCoroutine()
    {
        canTakeDamage = true;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = false;
        hasTakenDamage = true;
        yield return new WaitForSeconds(1.0f);
        canTakeDamage = true;
    }

    private IEnumerator StopTakingDamageForOneSecond()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.0f);
        canTakeDamage = true;
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            DisablePlayer();
            DisableUI();
            DisableAllChildren(); // Disable children here
            StartCoroutine(DesaturateSceneCoroutine());
            DestroyEnemiesAndBullets();

            // Roll for revival chance
            if (Random.Range(0, 100) < revivalChance)
            {
                Debug.Log("Player revived!");
                Invoke("Revive", 6.0f); // Adjust this delay as needed
            }
            else
            {
                Debug.Log("Player did not revive.");
                Invoke("LoadDeathScreen", 3.0f); // Load DeathScreen after 3 seconds
            }
        }
    }

    private void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    private void Revive()
    {
        animator.SetBool("isRevived", true);
        animator.SetBool("isDead", false);

        // Restore health to maximum
        currentHealth = maxHealth;

        StartCoroutine(RestoreSaturation());

        // Do not enable children here; use animation events instead
        StartCoroutine(RevivalDelay());
    }

    private IEnumerator RevivalDelay()
    {
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("RevivalAnimation")); // Ensure RevivalAnimation is finished
        EnablePlayer();
        // The children will be re-enabled by the animation event
    }

    public void FullyRevived()
    {
        isRevived = false;
        animator.SetBool("isRevived", false);
    }

    private IEnumerator DesaturateSceneCoroutine()
    {
        float startTime = Time.time;

        while (Time.time < startTime + desaturationDuration && colorAdjustments != null)
        {
            float t = (Time.time - startTime) / desaturationDuration;
            colorAdjustments.saturation.value = Mathf.Lerp(100.0f, -100.0f, t);
            yield return null;
        }
    }

    private IEnumerator RestoreSaturation()
    {
        float startTime = Time.time;

        while (Time.time < startTime + desaturationDuration && colorAdjustments != null)
        {
            float t = (Time.time - startTime) / desaturationDuration;
            colorAdjustments.saturation.value = Mathf.Lerp(-100.0f, 100.0f, t);
            yield return null;
        }
    }

    private void DisablePlayer()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
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

        DisableUI();  // UI disabling is enough
    }

    private void EnablePlayer()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f;
            rb.isKinematic = false;
        }

        PMovement2 myScriptComponentOne = GetComponent<PMovement2>();
        if (myScriptComponentOne != null)
        {
            myScriptComponentOne.enabled = true;
        }

        Shooting myScriptComponentTwo = GetComponent<Shooting>();
        if (myScriptComponentTwo != null)
        {
            myScriptComponentTwo.enabled = true;
        }

        AimMouseWeapon myScriptComponentThree = GetComponent<AimMouseWeapon>();
        if (myScriptComponentThree != null)
        {
            myScriptComponentThree.enabled = true;
        }
    }

    public void StartBlocking()
    {
        if (!isBlocking && blockCooldownTimer <= 0.0f && (playerMovementScript == null || !playerMovementScript.playerDash))
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);
            blockCooldownTimer = blockCooldown;
            canTakeDamage = false;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }

            animator.SetTrigger("StopDamage");
        }
    }

    public void ResumeDamage()
    {
        canTakeDamage = true;
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

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public void RollHealthPause()
    {
        canTakeDamage = false;
    }

    public void RollHealthResume()
    {
        canTakeDamage = true;
    }

    private void ToggleInvincibility()
    {
        isInvincible = !isInvincible;

        if (invincibilityText != null)
        {
            invincibilityText.gameObject.SetActive(isInvincible);
        }
    }

    private void DestroyEnemiesAndBullets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    // Method to get the current revival chance
    public int GetRevivalChance()
    {
        return revivalChance;
    }

    // Method to set the revival chance
    public void SetRevivalChance(int chance)
    {
        revivalChance = Mathf.Clamp(chance, 0, 100);
        Debug.Log("Revival chance set to: " + revivalChance + "%");
    }

    // Animation event functions
    public void DisableChildren()
    {
        DisableAllChildren();
    }

    public void EnableChildren()
    {
        EnableAllChildren();
    }

    private void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void EnableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    // New method to update health icon based on player's current health
    private void UpdateHealthIcon()
    {
        if (healthIconAnimator != null)
        {
            if (currentHealth > 300)
            {
                healthIconAnimator.SetBool("strongHead", true);
                healthIconAnimator.SetBool("damagedHead", false);
                healthIconAnimator.SetBool("weakHead", false);
            }
            else if (currentHealth > 100)
            {
                healthIconAnimator.SetBool("strongHead", false);
                healthIconAnimator.SetBool("damagedHead", true);
                healthIconAnimator.SetBool("weakHead", false);
            }
            else
            {
                healthIconAnimator.SetBool("strongHead", false);
                healthIconAnimator.SetBool("damagedHead", false);
                healthIconAnimator.SetBool("weakHead", true);
            }
        }
    }
}
