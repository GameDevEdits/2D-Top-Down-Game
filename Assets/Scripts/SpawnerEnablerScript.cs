using System.Collections;
using UnityEngine;

public class SpawnerEnablerScript : MonoBehaviour
{
    public GameObject enemySpawnerObject; // Drag and drop the EnemySpawner GameObject in the Inspector
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
        if (enemySpawnerObject != null)
        {
            EnemySpawner enemySpawner = enemySpawnerObject.GetComponent<EnemySpawner>();

            Debug.Log("Waiting 10 seconds");
            // Step 2: Wait for 10 seconds
            yield return new WaitForSeconds(10f);

            Debug.Log("20-second wave 1");
            // Step 3: Enable for 20 seconds
            enemySpawner.SpawnEnemies();
            yield return new WaitForSeconds(20f);

            Debug.Log("Waiting 10 seconds");
            // Step 4: Disable for 10 seconds
            enemySpawnerObject.SetActive(false);
            yield return new WaitForSeconds(10f);

            Debug.Log("20-second wave 2");
            // Step 5: Enable for 20 seconds
            enemySpawner.SpawnEnemies();
            enemySpawnerObject.SetActive(true);
            yield return new WaitForSeconds(20f);

            Debug.Log("Waves completed!");
            // Step 8: Disable
            enemySpawnerObject.SetActive(false);
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
