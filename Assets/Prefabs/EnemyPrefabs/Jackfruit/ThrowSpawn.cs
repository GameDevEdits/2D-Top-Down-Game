using UnityEngine;

public class ThrowSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign the prefab in the Unity Editor
    public Transform spawnPoint; // Assign the spawn point transform in the Unity Editor
    public float moveSpeed = 2f; // Speed at which the prefab moves upwards

    private GameObject spawnedObject;

    // This method will be called by an animation event
    public void SpawnThrowObject()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // Spawn the prefab at the spawn point
            spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

            // Destroy the spawned object after 5 seconds
            Destroy(spawnedObject, 5f);
        }
        else
        {
            Debug.LogError("Prefab or Spawn Point not assigned!");
        }
    }

    private void Update()
    {
        if (spawnedObject != null)
        {
            // Move the spawned object upwards
            MoveUpwards();
        }
    }

    // Move the object upwards
    private void MoveUpwards()
    {
        // Continuously move the object upwards
        Vector3 newPosition = spawnedObject.transform.position + Vector3.up * moveSpeed * Time.deltaTime;
        spawnedObject.transform.position = newPosition;
    }
}
