using System.Collections;
using UnityEngine;

public class CDonDeath : MonoBehaviour
{
    public float radius = 5f;
    public int damageAmount = 50;
    public float damageCooldown = 1f;
    public Vector2 offset;

    private bool isDamageCircleActive = false;
    private bool canDealDamage = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }

    private void Update()
    {
        if (isDamageCircleActive)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll((Vector2)transform.position + offset, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player") && canDealDamage)
                {
                    PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damageAmount);
                        StartCoroutine(DamageCooldown());
                    }
                }
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }

    public void ActivateDmgCircle()
    {
        isDamageCircleActive = true;
    }

    public void EndDmgCircle()
    {
        isDamageCircleActive = false;
    }
}
