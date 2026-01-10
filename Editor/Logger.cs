using UnityEditor;
using UnityEngine;

public static class Logger
{
    public static void Log(string message)
    {
        var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings.asset");
        if (settings != null && settings.debugLogs == true) 
        {
            Debug.Log(message);
        } else
        {
            Debug.Log(message);
        }
    }

    public static void LogWarning(string message)
    {
        var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings.asset");
        if (settings != null && settings.warningLogs == true)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.LogWarning(message);
        }
    }

    public static void LogError(string message)
    {
        var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings.asset");
        if (settings != null && settings.errorLogs == true)
        {
            Debug.LogError(message);
        }
        else
        {
            Debug.LogError(message);
        }
    }
}
