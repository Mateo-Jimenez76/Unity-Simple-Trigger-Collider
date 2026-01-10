using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
public class CustomSettingsProvider : SettingsProvider
{
    private SerializedObject SerializedSettings;

    public const string mySettingsPath = "Assets/Resources/SimpleTriggerColliderSettings.asset";

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
    }

    //Renders the settings GUI
    public override void OnGUI(string searchContext)
    {
        SerializedSettings.Update();
        EditorGUILayout.PropertyField(SerializedSettings.FindProperty("debugLogs"));
        EditorGUILayout.PropertyField(SerializedSettings.FindProperty("defaultColliderType"));
        EditorGUILayout.PropertyField(SerializedSettings.FindProperty("defaultCollider2DType"));
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
