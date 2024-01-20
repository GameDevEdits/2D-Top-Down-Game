using UnityEngine;

public class AffectedArchwayController : MonoBehaviour
{
    public Animator animator;
    public string playerTag = "Player";
    public float activationRadius = 5.0f;

    private bool wavesCompleted = false;

    public void SetWavesCompleted(bool completed)
    {
        wavesCompleted = completed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && wavesCompleted)
        {
            if (animator != null)
            {
                animator.SetBool("openArchway", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (animator != null)
            {
                animator.SetBool("openArchway", false);
            }
        }
    }

    public void OpenArchway()
    {
        animator.SetBool("openArchway", true);
    }
}
