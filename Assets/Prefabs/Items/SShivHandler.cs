using UnityEngine;

public class SShivHandler : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered the trigger has the tag "SShiv".
        if (other.CompareTag("SShiv"))
        {
            // Find the BulletController script on the bullet prefab.
            BulletController bulletController = bulletPrefab.GetComponent<BulletController>();

            // Check if the BulletController script is found.
            if (bulletController != null)
            {
                // Multiply the damage by 1.25 (increase by 25%).
                bulletController.damage = Mathf.CeilToInt(bulletController.damage * 1.25f);
                Debug.Log("Bullet damage increased by 25%");
            }
            else
            {
                Debug.LogWarning("BulletController script not found on the prefab.");
            }
        }
    }
}
