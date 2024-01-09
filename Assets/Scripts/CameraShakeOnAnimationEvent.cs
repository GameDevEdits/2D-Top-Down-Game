using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShakeOnAnimationEvent : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeDuration = 0.5f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    // This method can be called from an Animation Event in your animation clip
    public void StartCameraShake()
    {
        if (virtualCamera != null)
        {
            StartCoroutine(ShakeCameraCoroutine());
        }
        else
        {
            Debug.LogWarning("CinemachineVirtualCamera not assigned!");
        }
    }

    IEnumerator ShakeCameraCoroutine()
    {
        // Get the Cinemachine Basic MultiChannelPerlin noise module
        CinemachineBasicMultiChannelPerlin noiseModule = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noiseModule != null)
        {
            // Set the noise values for the camera shake
            noiseModule.m_AmplitudeGain = shakeAmplitude;
            noiseModule.m_FrequencyGain = shakeFrequency;

            // Wait for the specified duration
            yield return new WaitForSeconds(shakeDuration);

            // Reset the noise values after the shake duration
            noiseModule.m_AmplitudeGain = 0f;
            noiseModule.m_FrequencyGain = 0f;
        }
        else
        {
            Debug.LogWarning("CinemachineBasicMultiChannelPerlin not found on the CinemachineVirtualCamera!");
        }
    }
}
