using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of game objects to spawn.
    public float spawnRadius = 10f;     // The radius within which objects will be spawned.
    public float spawnInterval = 2f;    // Time interval between spawns.

    private Transform playerTransform;

    private void Start()
    {
        // Find the player's transform.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Start spawning objects repeatedly only if the spawner is enabled.
        if (gameObject.activeSelf && enabled)
        {
            InvokeRepeating("SpawnObject", 0f, spawnInterval);
        }
    }

    private void SpawnObject()
    {
        // Check if the spawner is enabled before attempting to spawn.
        if (!gameObject.activeSelf || !enabled)
        {
            return;
        }

        // Calculate a random position within the spawn radius.
        Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomPosition.x, randomPosition.y, 0f);

        // Choose a random object from the array to spawn.
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Instantiate the selected object at the calculated position.
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
