using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float[] spawnChances;
    public float spawnRadius = 15f; // Radius within which objects will be spawned.
    public int numberOfEnemiesToSpawn = 5; // Number of enemies to spawn.

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (!gameObject.activeSelf || !enabled)
        {
            return;
        }

        // Ensure the number of enemies to spawn is not greater than the available options.
        numberOfEnemiesToSpawn = Mathf.Min(numberOfEnemiesToSpawn, objectsToSpawn.Length);

        // Shuffle spawn chances and objects array.
        ShuffleArrays();

        for (int spawnCount = 0; spawnCount < numberOfEnemiesToSpawn; spawnCount++)
        {
            // Calculate a random position within the spawn radius.
            Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = playerTransform.position + new Vector3(randomPosition.x, randomPosition.y, 0f);

            // Instantiate the selected object at the calculated position.
            Instantiate(objectsToSpawn[spawnCount], spawnPosition, Quaternion.identity);
        }
    }

    private void ShuffleArrays()
    {
        // Simple array shuffle to randomize the order.
        for (int i = objectsToSpawn.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            // Swap objectsToSpawn array.
            GameObject tempObject = objectsToSpawn[i];
            objectsToSpawn[i] = objectsToSpawn[j];
            objectsToSpawn[j] = tempObject;

            // Swap spawnChances array.
            float tempChance = spawnChances[i];
            spawnChances[i] = spawnChances[j];
            spawnChances[j] = tempChance;
        }
    }
}
