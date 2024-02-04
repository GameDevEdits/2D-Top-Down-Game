using UnityEngine;

[System.Serializable]
public class SpawnableItem
{
    public GameObject itemPrefab;
    [Range(0f, 1f)]
    public float spawnChance;
}
