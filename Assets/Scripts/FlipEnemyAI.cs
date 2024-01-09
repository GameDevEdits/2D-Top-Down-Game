using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemyAI : MonoBehaviour
{

    public bool flip;

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
        }

        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }
}
