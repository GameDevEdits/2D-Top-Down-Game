using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public string targetObjectName = "TransformOrangeHD";  // The name of the target GameObject to follow
    public float smoothSpeed = 5f;                          // Smoothing factor for movement

    private Transform target;

    void Start()
    {
        // Find the target GameObject with the specified name
        GameObject targetObject = GameObject.Find(targetObjectName);

        // Check if the target GameObject is found
        if (targetObject != null)
        {
            // Get the Transform component of the target GameObject
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("Target GameObject not found with name: " + targetObjectName);
        }
    }

    void Update()
    {
        // Check if the target Transform is assigned
        if (target != null)
        {
            // Calculate the position the GameObject should move towards
            Vector3 targetPosition = target.position;

            // Use Mathf.Lerp to smoothly interpolate between current position and target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            // Update the GameObject's position
            transform.position = smoothedPosition;
        }
        // Uncomment the else block if you want to display a warning in the console
        // else
        // {
        //     Debug.LogWarning("Target Transform not assigned.");
        // }
    }
}
