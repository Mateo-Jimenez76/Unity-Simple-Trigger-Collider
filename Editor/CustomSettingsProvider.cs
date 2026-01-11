using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
public class CustomSettingsProvider : SettingsProvider
{
    private SerializedObject SerializedSettings;

    public const string mySettingsPath = "Assets/Resources/SimpleTriggerColliderSettings.asset";

    SerializedProperty debugLogsProp;
    SerializedProperty warningLogsProp;
    SerializedProperty errorLogsProp;
    SerializedProperty defaultColliderTypeProp;
    SerializedProperty defaultCollider2DTypeProp;

    //Constructor
    //SettingsScope determines where the settings are stored (User(Preferences) or Project(Project Settings))
    //Project scope is only effects the current project, while User scope is global to the user
    public CustomSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
        : base(path, scope) { }

    public static bool IsSettingsAvailable()
    {
        return File.Exists(mySettingsPath);
    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        SerializedSettings = CustomSettings.GetSerializedSettings();

        debugLogsProp = SerializedSettings.FindProperty("debugLogs");
        warningLogsProp = SerializedSettings.FindProperty("warningLogs");
        errorLogsProp = SerializedSettings.FindProperty("errorLogs");
        defaultColliderTypeProp = SerializedSettings.FindProperty("defaultColliderType");
        defaultCollider2DTypeProp = SerializedSettings.FindProperty("defaultCollider2DType");
    }

    //Renders the settings GUI
    public override void OnGUI(string searchContext)
    {
        SerializedSettings.Update();
        EditorGUILayout.PropertyField(debugLogsProp, new GUIContent("Debug Logs", "Enables or disables debug logs."));
        EditorGUILayout.PropertyField(warningLogsProp, new GUIContent("Warning Logs", "Enables or disables warning logs."));
        EditorGUILayout.PropertyField(errorLogsProp, new GUIContent("Error Logs", "Enables or disables error logs."));
        EditorGUILayout.PropertyField(defaultColliderTypeProp, new GUIContent("Default Collider", "The Collider that is automatically created, when one is missing, on an object that has TriggerCollider.cs attached to it."));
        EditorGUILayout.PropertyField(defaultCollider2DTypeProp, new GUIContent("Default Collider2D","The Collider2D that is automatically created, when one is missing, on an objec that has TriggerCollider2D.cs attached to it."));
        SerializedSettings.ApplyModifiedPropertiesWithoutUndo();
    }

    //Tells unity to render the given SettingsProvider to a window
    [SettingsProvider]
    public static SettingsProvider CreateCustomSettingsProvider()
    {
        //The path is what shows up in the Settings window
        var provider = new CustomSettingsProvider("Project/Simple Trigger Collider", SettingsScope.Project);
        return provider;
    }
}
