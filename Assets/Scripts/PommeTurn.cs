using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PommeTurn : MonoBehaviour
{
	public bool flip;
	private Animator animator;
	private bool isTurning;
	private Rigidbody2D rb;
	private bool isFrozen;
	private Transform playerTransform;
	private bool faceRight;
	private bool faceLeft;
	
	[SerializeField]private EnemyAI enemyAi;
	
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		
		if(playerTransform.transform.position.x > transform.position.x)
		{
			Vector3 scale = transform.localScale;
			faceRight = true;
			faceLeft = false;
			scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
			transform.localScale = scale;
		}
		else
		{
			Vector3 scale = transform.localScale;
			faceRight = false;
			faceLeft = true;
			scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
			transform.localScale = scale;
		}
    }

    public void Update()
    {
		if(!isTurning)
		{
			if(playerTransform.transform.position.x > transform.position.x && !faceRight && faceLeft)
			{
				isTurning = true;
				animator.SetBool("IsTurn", true);
				
				StartCoroutine(ResetTurnRight());
			}
		
			else if(playerTransform.transform.position.x < transform.position.x && !faceLeft && faceRight)
			{	
				isTurning = true;
				animator.SetBool("IsTurn", true);
		
				StartCoroutine(ResetTurnLeft());
			}
		}
    }
	
	private IEnumerator ResetTurnRight()
	{
		yield return new WaitForSeconds(0.7f);
		
		if(animator != null)
		{
			animator.SetBool("IsTurn", false);
		}
		
		isTurning = false;

		Vector3 scale = transform.localScale;
		faceRight = true;
		faceLeft = false;
		scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
		transform.localScale = scale;
	}
	
	private IEnumerator ResetTurnLeft()
	{
		yield return new WaitForSeconds(0.7f);
		
		if(animator != null)
		{
			animator.SetBool("IsTurn", false);
		}
		
		isTurning = false;
		
		Vector3 scale = transform.localScale;
		faceRight = true;
		faceLeft = false;
		scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
		transform.localScale = scale;
	}
	
	public bool IsTurning()
	{
		return isTurning;
	}
	
	public void Freeze()
	{
		EnemyAI enemyAi = GetComponent<EnemyAI>();
		if(enemyAi != null)
		{
			enemyAi.enabled = false;
		}
		if(!isFrozen && rb != null)
		{
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFrozen = true;
		}
		
		Move();
	}
	
	public void Move()
	{
		EnemyAI enemyAi = GetComponent<EnemyAI>();
		if(enemyAi != null)
		{
			enemyAi.enabled = true;
		}
		if (isFrozen && rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            isFrozen = false;
        }
	}
}
