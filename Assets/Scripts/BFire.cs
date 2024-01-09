using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFire : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private int damage;

    public void Init(Vector2 dir, float bulletSpeed, int bulletDamage)
    {
        direction = dir;
        speed = bulletSpeed;
        damage = bulletDamage;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy")) // Destroy the bullet when hitting something other than the player or other enemies.
        {
            Destroy(gameObject);
        }
    }
}
