using UnityEngine;
using UnityEditor;
public class CustomSettings : ScriptableObject
{
    // Path to the asset that will hold the settings information
    // "Asset/Editor" restricts your ability to use these settings at runtime
    // So "Assets/Resources" is a better location if runtime access is needed
    public const string settingsPath = "Assets/Resources/CustomSettings.asset";

    // --- Settings for your package ---
    [SerializeField] private bool debugLogs;

    [SerializeField] private ColliderType defaultColliderType;

    [SerializeField] private Collider2DType defaultCollider2DType;

    internal static CustomSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<CustomSettings>(settingsPath);

        if (settings == null)
        {
            //Create an instance of the settings object
            settings = CreateInstance<CustomSettings>();

            //Set default values for settings
            settings.debugLogs = true;
            settings.defaultColliderType = ColliderType.Box;
            settings.defaultCollider2DType = Collider2DType.Box;

            //Save the settings object as an asset
            AssetDatabase.CreateAsset(settings, settingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }

    private enum ColliderType
    {
        Box,
        Sphere,
        Capsule,
        Mesh
    }

    private enum Collider2DType
    {
        Box,
        Circle,
        Polygon,
        Edge
    }
}
