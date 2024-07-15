using UnityEngine;
using TMPro;

public class AffectedArchwayController : MonoBehaviour
{
    public Animator animator;
    public string playerTag = "Player";
    public float activationRadius = 5.0f;

    private bool wavesCompleted = false;
    private bool isArchwayOpen = false; // Instance variable to track archway state

    public void SetWavesCompleted(bool completed)
    {
        wavesCompleted = completed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && wavesCompleted && !isArchwayOpen)
        {
            if (animator != null)
            {
                animator.SetBool("openArchway", true);
                isArchwayOpen = true; // Mark this instance as open
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && isArchwayOpen)
        {
            if (animator != null)
            {
                animator.SetBool("openArchway", false);
                isArchwayOpen = false; // Mark this instance as closed
            }
        }
    }

    public void OpenArchway()
    {
        if (!isArchwayOpen)
        {
            animator.SetBool("openArchway", true);
            isArchwayOpen = true; // Mark this instance as open
        }
    }
}
