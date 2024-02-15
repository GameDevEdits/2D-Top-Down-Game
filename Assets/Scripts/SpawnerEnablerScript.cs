using System.Collections;
using UnityEngine;

public class SpawnerEnablerScript : MonoBehaviour
{
    public GameObject wave1SpawnerObject;
    public GameObject wave2SpawnerObject;
    public GameObject archwayBlocker;
    public GameObject specificArchway;

    public GameObject wave1Icon;
    public GameObject wave1Text;
    public GameObject wave2Icon;
    public GameObject wave2Text;
    public GameObject wavesCompletedIcon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateAndDeactivate());
        }
    }

    private IEnumerator ActivateAndDeactivate()
    {
        // Disable collider
        GetComponent<Collider2D>().enabled = false;

        if (wave1SpawnerObject != null && wave2SpawnerObject != null)
        {
            EnemySpawner wave1Spawner = wave1SpawnerObject.GetComponent<EnemySpawner>();
            EnemySpawner wave2Spawner = wave2SpawnerObject.GetComponent<EnemySpawner>();

            // Step 1: Enable Wave1Icon
            wave1Icon.SetActive(true);
            wave1Text.SetActive(true);

            yield return new WaitForSeconds(3f);

            // Step 2: Disable Wave1Icon

            Debug.Log("20-second wave 1");

            // Step 3: Start Wave 1
            wave1Spawner.SpawnEnemies();
            wave1SpawnerObject.SetActive(true);
            float wave1Timer = 15f;
            while (wave1Timer > 0f)
            {
                wave1Timer -= Time.deltaTime;
                yield return null;
            }

            // Step 5: Start Wave 2
            wave2Icon.SetActive(true);
            wave1Text.SetActive(false);
            wave2Text.SetActive(true);
            Debug.Log("20-second wave 2");
            wave2Spawner.SpawnEnemies();
            wave2SpawnerObject.SetActive(true);
            float wave2Timer = 20f;
            while (wave2Timer > 0f)
            {
                wave2Timer -= Time.deltaTime;
                yield return null;
            }

            wave1Icon.SetActive(false);
            wave2Icon.SetActive(false);
            wave2Text.SetActive(false);

            // Step 6: Enable WavesCompletedIcon and open archway
            wavesCompletedIcon.SetActive(true);
            archwayBlocker.SetActive(false);

            // Set waves completed for the specific archway
            if (specificArchway != null)
            {
                AffectedArchwayController affectedArchwayController = specificArchway.GetComponent<AffectedArchwayController>();
                if (affectedArchwayController != null)
                {
                    affectedArchwayController.SetWavesCompleted(true);
                    affectedArchwayController.OpenArchway();
                }
            }

            yield return new WaitForSeconds(5f);
            wavesCompletedIcon.SetActive(false);
        }
    }
}
