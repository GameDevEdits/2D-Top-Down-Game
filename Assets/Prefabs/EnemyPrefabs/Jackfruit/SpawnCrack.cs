using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrack : MonoBehaviour
{
    public GameObject prefabToSpawn; // Prefab to spawn
    public Transform spawnPoint; // Transform where the prefab will spawn

    // Method to be called from the animation event
    public void SpawnCrackPrefab()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab or Spawn Point not assigned!");
        }
    }
}
