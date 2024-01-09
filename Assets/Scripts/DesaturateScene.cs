using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DesaturateScene : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    private float desaturationDuration = 3.0f;

    private void Start()
    {
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();

        if (globalVolume != null && globalVolume.profile.TryGet(out colorAdjustments))
        {
            // Start the desaturation process
            StartCoroutine(DesaturateSceneCoroutine());
        }
        else
        {
            Debug.LogError("Volume or ColorAdjustments settings not found on the 'Global Volume' GameObject.");
        }
    }

    IEnumerator DesaturateSceneCoroutine()
    {
        float startTime = Time.time;

        while (Time.time < startTime + desaturationDuration && colorAdjustments != null)
        {
            float t = (Time.time - startTime) / desaturationDuration;

            // Linearly interpolate between the current saturation and -100 (completely desaturated)
            colorAdjustments.saturation.value = Mathf.Lerp(0.0f, -100.0f, t);

            yield return null;
        }
    }
}
