using System.Collections;
using UnityEngine;

public class DumbellDownSlam : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign the prefab in the Unity Editor
    public float delayBeforeImpact = 2f; // Delay before setting Impact parameter to true
    public float spawnHeight = 20f; // Height above the player where the dumbell will spawn

    // This method will be called by an animation event
    public void SpawnDumbellDown()
    {
        if (prefabToSpawn != null)
        {
            // Find the player object by tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Calculate the spawn position
                Vector3 spawnPosition = player.transform.position + Vector3.up * spawnHeight;

                // Spawn the prefab at the calculated position
                GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

                // Get the DumbellController component from the spawned object
                DumbellController dumbellController = spawnedObject.GetComponent<DumbellController>();

                // Start the impact delay coroutine for the spawned object
                StartCoroutine(ImpactDelay(dumbellController));
            }
            else
            {
                Debug.LogError("Player object not found!");
            }
        }
        else
        {
            Debug.LogError("Prefab not assigned!");
        }
    }

    // Coroutine to set "Impact" parameter after a delay
    private IEnumerator ImpactDelay(DumbellController dumbellController)
    {
        yield return new WaitForSeconds(delayBeforeImpact);

        // Set "Impact" parameter to true in the DumbellController
        if (dumbellController != null)
        {
            dumbellController.Impact();
        }
        else
        {
            Debug.LogWarning("No DumbellController found on the spawned object. Impact parameter not set.");
        }
    }
}
