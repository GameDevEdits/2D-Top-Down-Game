using UnityEngine;
using System.Collections.Generic;

public class ChestController : MonoBehaviour
{
    public Animator chestAnimator;
    public float interactionRadius = 2f;
    public LayerMask playerLayer;
    public GameObject[] itemPrefabs;

    private bool isOpened = false;

    void Update()
    {
        if (!isOpened && Input.GetButtonDown("Interact"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius, playerLayer);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    OpenChest();
                    break;
                }
            }
        }
    }

    void OpenChest()
    {
        isOpened = true; // Prevents repeated opening

        chestAnimator.SetBool("OpenChest", true);

        // Spawn a random item
        if (itemPrefabs != null && itemPrefabs.Length > 0)
        {
            GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(randomItemPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Item prefabs array is empty or null.");
        }
    }

    // Draw the interaction radius in the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
