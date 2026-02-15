using UnityEngine;

namespace SimpleTriggerCollider.Editor
{
    public static class PackageLogger
    {
        public static void Log(string message)
        {
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings");
            if (settings != null && settings.DebugLogsEnabled() == true)
            {
                Debug.Log(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

        public static void LogWarning(string message)
        {
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings");
            if (settings != null && settings.WarningLogsEnabled() == true)
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
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings");
            if (settings != null && settings.ErrorLogsEnabled() == true)
            {
                Debug.LogError(message);
            }
            else
            {
                Debug.LogError(message);
            }
        }
    }
}
