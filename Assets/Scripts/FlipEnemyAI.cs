using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemyAI : MonoBehaviour
{
    public bool flip;

    private Transform playerTransform;
    private bool canFlip = false;
    private float delayTimer = 2f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(EnableFlipping());
    }

    private IEnumerator EnableFlipping()
    {
        yield return new WaitForSeconds(delayTimer);
        canFlip = true;
    }

    private void Update()
    {
        if (!canFlip)
            return;

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
