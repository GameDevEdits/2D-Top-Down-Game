using System.Collections;
using UnityEngine;

public class SpawnerEnablerScript : MonoBehaviour
{
    public GameObject objectToActivate; // Drag and drop the GameObject you want to activate in the Inspector
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
        if (objectToActivate != null)
        {
            Debug.Log("Waiting 10 seconds");
            // Step 2: Wait for 10 seconds
            yield return new WaitForSeconds(10f);

            Debug.Log("20-second wave 1");
            // Step 3: Enable for 20 seconds
            objectToActivate.SetActive(true);
            yield return new WaitForSeconds(20f);

            Debug.Log("Waiting 10 seconds");
            // Step 4: Disable for 10 seconds
            objectToActivate.SetActive(false);
            yield return new WaitForSeconds(10f);

            Debug.Log("20-second wave 2");
            // Step 5: Enable for 20 seconds
            objectToActivate.SetActive(true);
            yield return new WaitForSeconds(20f);

            Debug.Log("Waves completed!");
            // Step 8: Disable
            objectToActivate.SetActive(false);
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
