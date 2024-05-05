using System.Collections;
using UnityEngine;

public class DumbellDownSlam : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign the prefab in the Unity Editor
    public Transform spawnPoint; // Assign the spawn point transform in the Unity Editor
    public float delayBeforeImpact = 2f; // Delay before setting Impact parameter to true

    // This method will be called by an animation event
    public void SpawnDumbellDown()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // Spawn the prefab at the spawn point
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

            // Get the DumbellController component from the spawned object
            DumbellController dumbellController = spawnedObject.GetComponent<DumbellController>();

            // Start the impact delay coroutine for the spawned object
            StartCoroutine(ImpactDelay(dumbellController));
        }
        else
        {
            Debug.LogError("Prefab or Spawn Point not assigned!");
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
