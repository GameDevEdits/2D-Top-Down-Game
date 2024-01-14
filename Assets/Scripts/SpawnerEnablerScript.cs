using System.Collections;
using UnityEngine;

public class SpawnerEnablerScript : MonoBehaviour
{
    public GameObject wave1SpawnerObject; // Drag and drop the first EnemySpawner GameObject in the Inspector
    public GameObject wave2SpawnerObject; // Drag and drop the second EnemySpawner GameObject in the Inspector
    public GameObject archwayBlocker;
    public GameObject specificArchway; // Drag and drop the specific Archway GameObject you want to control

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateAndDeactivate());
        }
    }

    private IEnumerator ActivateAndDeactivate()
    {
        if (wave1SpawnerObject != null && wave2SpawnerObject != null)
        {
            EnemySpawner wave1Spawner = wave1SpawnerObject.GetComponent<EnemySpawner>();
            EnemySpawner wave2Spawner = wave2SpawnerObject.GetComponent<EnemySpawner>();

            Debug.Log("Waiting 3 seconds");
            // Step 2: Wait for 10 seconds
            yield return new WaitForSeconds(3f);

            Debug.Log("20-second wave 1");
            // Step 3: Enable Wave 1 for 20 seconds
            wave1Spawner.SpawnEnemies();
            wave1SpawnerObject.SetActive(true);
            yield return new WaitForSeconds(20f);

            Debug.Log("Waiting 10 seconds");
            // Step 4: Disable Wave 1 for 10 seconds
            wave1SpawnerObject.SetActive(false);
            yield return new WaitForSeconds(10f);

            Debug.Log("20-second wave 2");
            // Step 5: Enable Wave 2 for 20 seconds
            wave2Spawner.SpawnEnemies();
            wave2SpawnerObject.SetActive(true);
            yield return new WaitForSeconds(20f);

            Debug.Log("Waves completed!");
            // Step 8: Disable Wave 2
            wave2SpawnerObject.SetActive(false);
            archwayBlocker.SetActive(false);

            // Set waves completed for the specific archway
            if (specificArchway != null)
            {
                AffectedArchwayController affectedArchwayController = specificArchway.GetComponent<AffectedArchwayController>();
                if (affectedArchwayController != null)
                {
                    affectedArchwayController.SetWavesCompleted(true);
                }
            }
        }
    }
}
