using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    private void Start()
    {
        // Ensure that CinemachineShake is initialized
        if (CinemachineShake.Instance == null)
        {
            Debug.LogError("CinemachineShake is not initialized. Make sure you have the CinemachineShake script in your project.");
        }
    }

    // This method is called from an Animation Event
    public void TriggerCameraShake()
    {
        // Shake the camera with specified intensity and duration
        CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
    }
}
