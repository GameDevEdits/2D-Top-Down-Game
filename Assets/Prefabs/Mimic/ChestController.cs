using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator animator;
    public List<SpawnableItem> itemsToSpawn;
    public Transform spawnPoint;

    public void OpenChest()
    {
        StartCoroutine(DelayedOpenChest());
    }

    IEnumerator DelayedOpenChest()
    {
        yield return new WaitForSeconds(0.5f);

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
            float totalSpawnChances = 0f;

            foreach (var item in itemsToSpawn)
            {
                totalSpawnChances += item.spawnChance;
            }

            float randomValue = Random.Range(0f, totalSpawnChances);

            foreach (var item in itemsToSpawn)
            {
                if (randomValue <= item.spawnChance)
                {
                    Vector3 spawnPosition = (spawnPoint != null) ? spawnPoint.position : transform.position;
                    Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                    break;
                }

                randomValue -= item.spawnChance;
            }
        }
    }
}