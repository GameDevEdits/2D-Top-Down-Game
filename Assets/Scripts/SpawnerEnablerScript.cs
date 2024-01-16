using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnerEnablerScript : MonoBehaviour
{
    public GameObject wave1SpawnerObject;
    public GameObject wave2SpawnerObject;
    public GameObject archwayBlocker;
    public GameObject specificArchway;

    public TextMeshProUGUI wave1StartText;
    public TextMeshProUGUI wave1TimerText;
    public TextMeshProUGUI wave1TimeCompletedText;
    public TextMeshProUGUI wave2StartingText;
    public TextMeshProUGUI wave2TimerText;
    public TextMeshProUGUI wavesCompletedText;

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

            // Step 1: Enable "Room 1: Wave 1" text
            wave1StartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);

            // Step 2: Disable "Room 1: Wave 1" text
            wave1StartText.gameObject.SetActive(false);

            Debug.Log("15-second wave 1");
            // Step 3: Enable Wave 1 Timer text for 15 seconds
            wave1TimerText.gameObject.SetActive(true);
            wave1Spawner.SpawnEnemies();
            wave1SpawnerObject.SetActive(true);
            float wave1Timer = 15f;
            while (wave1Timer > 0f)
            {
                wave1Timer -= Time.deltaTime;
                wave1TimerText.text = "Wave 1 Timer: " + Mathf.CeilToInt(wave1Timer);
                yield return null;
            }

            // Step 4: Disable Wave 1 Timer text
            wave1TimerText.gameObject.SetActive(false);

            // Step 5: Enable "Wave 1 Time Completed" and "Wave 2 Starting In: " text
            wave1TimeCompletedText.gameObject.SetActive(true);
            wave2StartingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);

            // Step 6: Disable "Wave 1 Time Completed" and "Wave 2 Starting In: " text
            wave1TimeCompletedText.gameObject.SetActive(false);
            wave2StartingText.gameObject.SetActive(false);

            Debug.Log("15-second wave 2");
            // Step 7: Enable Wave 2 Timer text for 15 seconds
            wave2TimerText.gameObject.SetActive(true);
            wave2Spawner.SpawnEnemies();
            wave2SpawnerObject.SetActive(true);
            float wave2Timer = 15f;
            while (wave2Timer > 0f)
            {
                wave2Timer -= Time.deltaTime;
                wave2TimerText.text = "Wave 2 Timer: " + Mathf.CeilToInt(wave2Timer);
                yield return null;
            }

            // Step 8: Disable Wave 2 Timer text
            wave2TimerText.gameObject.SetActive(false);

            // Step 9: Enable "Waves Completed!" text
            wavesCompletedText.gameObject.SetActive(true);
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

            yield return new WaitForSeconds(5f);
            wavesCompletedText.gameObject.SetActive(false);
        }
    }
}
