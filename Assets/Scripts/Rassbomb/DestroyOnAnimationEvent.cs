using UnityEngine;

public class DestroyOnAnimationEvent : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn

    // This function is called when the Animation Event "SpawnPrefabAndDestroy" is triggered in the animation
    public void SpawnPrefabAndDestroy()
    {
        // Get the current position and rotation of the game object
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Instantiate the prefab at the same position and rotation
        Instantiate(prefabToSpawn, position, rotation);

        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}