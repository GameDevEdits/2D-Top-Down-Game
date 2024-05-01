using UnityEngine;

public class PathScriptController : MonoBehaviour
{
    // Reference to the script you want to enable/disable
    public MonoBehaviour scriptToControl;

    // Method to enable the referenced script
    public void EnablePathScript()
    {
        if (scriptToControl != null)
        {
            scriptToControl.enabled = true;
        }
        else
        {
            Debug.LogWarning("Script reference is not set.");
        }
    }

    // Method to disable the referenced script
    public void DisablePathScript()
    {
        if (scriptToControl != null)
        {
            scriptToControl.enabled = false;
        }
        else
        {
            Debug.LogWarning("Script reference is not set.");
        }
    }
}
