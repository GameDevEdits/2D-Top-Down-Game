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

    void Update()
    {
        
    }
}
