using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Function to be called by an animation event to set isAttacking to true
    public void SetIsAttackingTrue()
    {
        anim.SetBool("isAttacking", true);
        // Freeze the position when isAttacking is true
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    // Function to be called by an animation event to set isAttacking to false
    public void SetIsAttackingFalse()
    {
        anim.SetBool("isAttacking", false);
        // Unfreeze the position when isAttacking is false
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Function to be called by an animation event to set isRunning to true
    public void SetIsRunningTrue()
    {
        anim.SetBool("isRunning", true);
    }

    // Function to be called by an animation event to set isRunning to false
    public void SetIsRunningFalse()
    {
        anim.SetBool("isRunning", false);
    }
}
