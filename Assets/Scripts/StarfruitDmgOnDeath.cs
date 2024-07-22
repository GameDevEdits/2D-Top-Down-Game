using System.Collections;
using UnityEngine;

public class StarfruitDmgOnDeath : MonoBehaviour
{
    public float radius1 = 5f;
    public float radius2 = 5f;
    public int damageAmount = 50;
    public float damageCooldown = 1f;
    public Vector2 offset1;
    public Vector2 offset2;

    private bool isDamageCircleActive = false;
    private bool canDealDamage = true;
    private bool isFlipped = false;

    private void OnDrawGizmos()
    {
        if (isFlipped)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)transform.position + offset2, radius2);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere((Vector2)transform.position + offset1, radius1);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)transform.position + offset1, radius1);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere((Vector2)transform.position + offset2, radius2);
        }
    }

    private void Update()
    {
        if (isDamageCircleActive)
        {
            if (isFlipped)
            {
                DealDamage((Vector2)transform.position + offset2, radius2);
            }
            else
            {
                DealDamage((Vector2)transform.position + offset1, radius1);
            }
        }
    }

    private void DealDamage(Vector2 position, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius);
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

    public void SetFlipped(bool flipped)
    {
        isFlipped = flipped;
    }
}
