using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemyAI : MonoBehaviour
{

    public bool flip;
	public bool faceRight;
	public bool faceLeft;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;

        if (playerTransform.transform.position.x > transform.position.x)
        {
			scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
			faceRight = true;
			faceLeft = false;
        }

        else
        {	
			scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
			faceRight = false;
			faceLeft = true;
        }

        transform.localScale = scale;
    }
}
