using UnityEngine;
using UnityEditor;
namespace SimpleTriggerCollider.Editor
{
    public class CustomSettings : ScriptableObject
    {
        // Path to the asset that will hold the settings information
        // "Asset/Editor" restricts your ability to use these settings at runtime
        // So "Assets/Resources" is a better location if runtime access is needed
        public const string settingsPath = "Assets/Resources/SimpleTriggerColliderSettings.asset";

        // --- Package Settings ---
        [SerializeField] private bool debugLogs;
        public bool DebugLogsEnabled() => debugLogs;

        [SerializeField] private bool warningLogs;
        public bool WarningLogsEnabled() => warningLogs;

        [SerializeField] private bool errorLogs;
        public bool ErrorLogsEnabled() => errorLogs;

        [SerializeField] private ColliderType defaultColliderType;
        public ColliderType GetDefaultColliderType() => defaultColliderType;

        [SerializeField] private Collider2DType defaultCollider2DType;
        public Collider2DType GetDefaultCollider2DType() => defaultCollider2DType;

        /// <summary>
        /// Retrieves the existing custom settings asset if it exists; otherwise, creates a new settings asset with default
        /// values and returns it.
        /// </summary>
        /// <remarks>If the settings asset does not exist at the expected path, this method creates the necessary
        /// folder structure and a new settings asset with default values. The method ensures that a valid settings asset is
        /// always returned.</remarks>
        /// <returns>A <see cref="CustomSettings"/> instance representing the current settings. If no settings asset exists, a new
        /// one is created and returned.</returns>
        internal static CustomSettings GetOrCreateSettings()
        {
            //Check that there is a valid location to store the settings.asset
            //If not...
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                Debug.LogWarning("<color=yellow>Created Resources folder</color> for Simple Trigger Collider settings at <color=cyan>Assets/Resources</color>");
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
                settings.warningLogs = true;
                settings.errorLogs = true;
                settings.defaultColliderType = ColliderType.Box;
                settings.defaultCollider2DType = Collider2DType.Box;

                //Save the settings object as an asset
                AssetDatabase.CreateAsset(settings, settingsPath);
                AssetDatabase.SaveAssets();
                Debug.LogWarning($"<color=yellow>Created Simple Trigger Collider settings object</color> at: <color=cyan>{settingsPath}</color>");
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

        public class Initializer : AssetPostprocessor
        {
            static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
            {
                //Ensure that the settings asset exists and is up to date
                CustomSettings.GetOrCreateSettings();
            }
        }
    }
}
