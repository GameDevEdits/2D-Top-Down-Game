using System.Collections;
using UnityEngine;

public class TrailMixHandler : MonoBehaviour
{
    public GameObject bulletPrefab;

    private void Start()
    {
        BulletController.ResetDamage(); // Ensure original damage is set on start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BulletController bulletController = bulletPrefab.GetComponent<BulletController>();
            if (bulletController != null)
            {
                // Increase the damage by 10%
                bulletController.damage = Mathf.CeilToInt(bulletController.damage * 1.1f);
                Debug.Log("Bullet damage increased to: " + bulletController.damage);
            }
        }
    }

    private void OnDisable()
    {
        // Reset the damage to the original value when the script or object is disabled
        BulletController bulletController = bulletPrefab.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.damage = BulletController.originalDamage;
        }
    }
}
