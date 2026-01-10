using UnityEditor;
using UnityEngine;

public static class Logger
{
    public static void Log(string message)
    {
        var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings.asset");
        if (settings.debugLogs == true) 
        {
            Debug.Log(message);
        }
    }

    public static void LogWarning(string message)
    {
        var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings.asset");
        if (settings.warningLogs == true)
        {
            Debug.LogWarning(message);
        }
    }

    public static void LogError(string message)
    {
        var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings.asset");
        if (settings.errorLogs == true)
        {
            Debug.LogError(message);
        }
    }
}
