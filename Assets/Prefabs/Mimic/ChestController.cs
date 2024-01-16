using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> itemsToSpawn;
    public Transform spawnPoint; // Set this in the Inspector to determine where items will spawn

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("ChestController: Animator is not assigned!");
        }
    }

    public void OpenChest()
    {
        StartCoroutine(DelayedOpenChest());
    }

    IEnumerator DelayedOpenChest()
    {
        // Wait for 0.5 seconds before opening the chest
        yield return new WaitForSeconds(0.5f);

        // Assuming you have an Animator parameter named "OpenChest" for chest opening animation
        if (animator != null)
        {
            animator.SetBool("OpenChest", true);
        }

        yield return new WaitForSeconds(0.1f);
        SpawnRandomItem();
    }

    void SpawnRandomItem()
    {
        if (itemsToSpawn.Count > 0)
        {
            int randomIndex = Random.Range(0, itemsToSpawn.Count);
            GameObject itemPrefab = itemsToSpawn[randomIndex];

            // Spawn the item at the specified spawn point or chest's position if spawnPoint is not set
            Vector3 spawnPosition = (spawnPoint != null) ? spawnPoint.position : transform.position;
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
