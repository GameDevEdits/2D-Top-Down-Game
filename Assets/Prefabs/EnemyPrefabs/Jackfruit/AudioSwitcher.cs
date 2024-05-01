using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToEnable;

    public void SwitchObjects()
    {
        // Check if both objects are assigned
        if (objectToDisable != null && objectToEnable != null)
        {
            // Disable the first object
            objectToDisable.SetActive(false);

            // Enable the second object
            objectToEnable.SetActive(true);
        }
        else
        {
            Debug.LogWarning("One or both GameObjects are not assigned!");
        }
    }

    public void SwitchObjectsBack()
    {
        // Check if both objects are assigned
        if (objectToEnable != null && objectToDisable != null)
        {
            // Disable the first object
            objectToEnable.SetActive(false);

            // Enable the second object
            objectToDisable.SetActive(true);
        }
        else
        {
            Debug.LogWarning("One or both GameObjects are not assigned!");
        }
    }
}
