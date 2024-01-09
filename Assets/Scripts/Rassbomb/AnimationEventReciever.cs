using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    // This function is called by the animation event
    public void OnAnimationEvent()
    {
        // Put your code here to execute when the animation event is called
        Debug.Log("Animation event called!");

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop rigidbody movement
            rb.gravityScale = 0f; // Disable gravity (if applicable)
            rb.isKinematic = true; // Make the rigidbody kinematic
        }

        // Disable the Collider2D component to prevent further interactions with the player.
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Disable the "MyScript" component.
        EnemyAI myScriptComponentOne = GetComponent<EnemyAI>();
        if (myScriptComponentOne != null)
        {
            myScriptComponentOne.enabled = false;
        }

        EnemyFlash myScriptComponentTwo = GetComponent<EnemyFlash>();
        if (myScriptComponentTwo != null)
        {
            myScriptComponentTwo.enabled = false;
        }

        NewEnemyMADShank myScriptComponentThree = GetComponent<NewEnemyMADShank>();
        if (myScriptComponentThree != null)
        {
            myScriptComponentThree.enabled = false;
        }

        EnemyMADShank myScriptComponentFour = GetComponent<EnemyMADShank>();
        if (myScriptComponentFour != null)
        {
            myScriptComponentFour.enabled = false;
        }

        FlipEnemyAI myScriptComponentFive = GetComponent<FlipEnemyAI>();
        if (myScriptComponentFive != null)
        {
            myScriptComponentFive.enabled = false;
        }

        NewMRAVisual myScriptComponentSix = GetComponent<NewMRAVisual>();
        if (myScriptComponentSix != null)
        {
            myScriptComponentSix.enabled = false;
        }

        NewEnemyMRA myScriptComponentSeven = GetComponent<NewEnemyMRA>();
        if (myScriptComponentSeven != null)
        {
            myScriptComponentSeven.enabled = false;
        }
    }
}
