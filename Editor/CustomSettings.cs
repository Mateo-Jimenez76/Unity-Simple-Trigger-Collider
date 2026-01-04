using UnityEngine;
using UnityEditor;
public class CustomSettings : ScriptableObject
{
    public const string settingsPath = "Assets/Resources/CustomSettings.asset";

    [SerializeField] private bool enableDebugLogs;


    internal static CustomSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<CustomSettings>(settingsPath);

        if (settings == null)
        {
            settings = CreateInstance<CustomSettings>();
            settings.enableDebugLogs = true;
            AssetDatabase.CreateAsset(settings, settingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}
