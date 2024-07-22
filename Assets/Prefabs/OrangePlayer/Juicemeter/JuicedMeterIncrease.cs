using System.Collections;
using UnityEngine;

public class JuicedMeterIncrease : MonoBehaviour
{
    public Animator powerMeterAnimator;
    private float currentFrameRate = 1f; // Default frame rate (1 frame per second)
    private bool isTemporaryBoostActive = false;

    void Start()
    {
        if (powerMeterAnimator == null)
        {
            Debug.LogError("Animator not assigned!");
            return;
        }

        // Start the animation coroutine
        StartCoroutine(AnimateMeter());
    }

    IEnumerator AnimateMeter()
    {
        while (true)
        {
            powerMeterAnimator.speed = 1; // Ensure the animator is running

            // Wait for the duration of one frame at the current frame rate
            yield return new WaitForSeconds(1f / currentFrameRate);

            powerMeterAnimator.speed = 0; // Pause the animation
            yield return null; // Ensure the frame advances
        }
    }

    public void ChangeFPS(float newFrameRate)
    {
        Debug.Log("Changing FPS to: " + newFrameRate);
        currentFrameRate = newFrameRate;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            Collider2D enemyCollider = collision.GetComponent<Collider2D>();
            if (enemyCollider != null && enemyCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (!isTemporaryBoostActive)
                {
                    StartCoroutine(TemporaryFrameRateBoost());
                }
            }
        }
    }

    IEnumerator TemporaryFrameRateBoost()
    {
        isTemporaryBoostActive = true;
        Debug.Log("Temporary frame rate boost activated.");

        float originalFrameRate = currentFrameRate;
        currentFrameRate = 10f; // Set frame rate to 10 fps
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        currentFrameRate = originalFrameRate; // Revert to the original frame rate
        isTemporaryBoostActive = false;
        Debug.Log("Temporary frame rate boost deactivated.");
    }
}
