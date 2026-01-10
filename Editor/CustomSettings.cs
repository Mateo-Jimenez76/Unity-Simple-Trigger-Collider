using UnityEngine;
using UnityEditor;
public class CustomSettings : ScriptableObject
{
    // Path to the asset that will hold the settings information
    // "Asset/Editor" restricts your ability to use these settings at runtime
    // So "Assets/Resources" is a better location if runtime access is needed
    public const string settingsPath = "Assets/Resources/SimpleTriggerColliderSettings.asset";

    // --- Settings for your package ---
    [field: SerializeField] public bool debugLogs { get; set; }

    [field: SerializeField] public bool warningLogs { get; set; }

    [field: SerializeField] public bool errorLogs { get; set; }

    [field: SerializeField] public ColliderType defaultColliderType { get; private set; }

    [field: SerializeField] public Collider2DType defaultCollider2DType { get; private set; }

    /// <summary>
    /// Retrieves the existing custom settings asset if it exists; otherwise, creates a new settings asset with default
    /// values and returns it.
    /// </summary>
    /// <remarks>If the settings asset does not exist at the expected path, this method creates the necessary
    /// folder structure and a new settings asset with default values. The method ensures that a valid settings asset is
    /// always returned, simplifying access to configuration data.</remarks>
    /// <returns>A <see cref="CustomSettings"/> instance representing the current settings. If no settings asset exists, a new
    /// one is created and returned.</returns>
    internal static CustomSettings GetOrCreateSettings()
    {
        //Check that there is a valid location to store the settings.asset
        var resources = AssetDatabase.LoadAssetAtPath<CustomSettings>("Assets/Resources");
        //If not...
        if (resources == null)
        {
            Logger.Log("Creating Resources folder for Simple Trigger Collider settings...");
            //...Create the Resources folder
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        //Try to load the settings asset
        var settings = AssetDatabase.LoadAssetAtPath<CustomSettings>(settingsPath);

        //If the settings asset does not exist...
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
            Logger.Log("Created Simple Trigger Collider settings at: " + settingsPath);
        }

        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }

    public enum ColliderType
    {
        Box,
        Sphere,
        Capsule,
        Mesh
    }

    public enum Collider2DType
    {
        Box,
        Circle,
        Polygon,
        Edge
    }
}
