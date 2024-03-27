using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float[] spawnChances;
    public float spawnRadius = 15f; // Radius within which objects will be spawned.
    public static int numberOfEnemiesToSpawn = 5; // Number of enemies to spawn.

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

        float totalChance = 0f;
        foreach (float chance in spawnChances)
        {
            totalChance += chance;
        }

        for (int spawnCount = 0; spawnCount < numberOfEnemiesToSpawn; spawnCount++)
        {
            float randomValue = Random.Range(0f, totalChance);
            float cumulativeChance = 0f;

            for (int i = 0; i < objectsToSpawn.Length; i++)
            {
                cumulativeChance += spawnChances[i];

                if (randomValue <= cumulativeChance)
                {
                    Vector3 spawnPosition = GetValidSpawnPosition();

                    // Instantiate the selected object at the calculated position.
                    Instantiate(objectsToSpawn[i], spawnPosition, Quaternion.identity);
                    break;
                }
            }
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 randomPosition;
        bool validPosition = false;

        do
        {
            // Calculate a random position within the spawn radius.
            randomPosition = Random.insideUnitCircle * spawnRadius;
            randomPosition += playerTransform.position;

            // Check the distance from already spawned enemies.
            validPosition = IsPositionValid(randomPosition);

        } while (!validPosition);

        return randomPosition;
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Check the distance from already spawned enemies.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 3f);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return false;
            }
        }

        return true;
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
