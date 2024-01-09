using UnityEngine;

public class MuteCollabLogs : MonoBehaviour
{
    void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Check for the Collab message and suppress it
        if (logString.Contains("[Collab] Collab service is deprecated"))
        {
            return;
        }

        // Log other messages
        Debug.unityLogger.Log(type, logString);
    }
}
