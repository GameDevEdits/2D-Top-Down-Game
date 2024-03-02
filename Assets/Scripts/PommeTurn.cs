using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PommeTurn : MonoBehaviour
{
    private Animator animator;
	private bool isTurning;
	private Rigidbody2D rb;
	private bool isFrozen;
	
	[SerializeField]private FlipEnemyAI flipEnemyAi;
	
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
    }

    public void TriggerTurn(Transform playerTransform)
    {
		if(!isTurning)
		{
				isTurning = true;
				
				if(animator != null)
				{
					animator.SetBool("IsTurn", true);
				}
				
				StartCoroutine(ResetTurn());
		}
    }
	
	private IEnumerator ResetTurn()
	{
		yield return new WaitForSeconds(1f);
		
		if(animator != null)
		{
			animator.SetBool("IsTurn", false);
		}
		
		isTurning = false;
		
		if(isFrozen && rb != null)
		{
			rb.gameObject.SendMessage("Move", SendMessageOptions.DontRequireReceiver);
            isFrozen = false;
		}
	}
	
	public bool IsTurning()
	{
		return isTurning;
	}
	
	public void Freeze()
	{
		if(flipEnemyAi != null)
		{
			flipEnemyAi.enabled = false;
		}
		
		if(!isFrozen && rb != null)
		{
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFrozen = true;
		}
	}
	
	public void Move()
	{
		if(flipEnemyAi != null)
		{
			flipEnemyAi.enabled = true;
		}
		
		if (isFrozen && rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            isFrozen = false;
        }
	}
}
