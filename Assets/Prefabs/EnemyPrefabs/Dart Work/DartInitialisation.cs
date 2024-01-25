using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartInitialisation : MonoBehaviour
{
    void Start()
    {
        RotateToFacePlayer();
    }

    void RotateToFacePlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            Vector2 playerDirection = playerObject.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player object has the 'Player' tag.");
        }
    }
}

