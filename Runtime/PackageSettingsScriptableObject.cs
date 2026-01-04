using UnityEngine;

[CreateAssetMenu(fileName = "PackageSettings", menuName = "SimpleTriggerColliders/Package Settings Scriptable Object")]
public class PackageSettingsScriptableObject : ScriptableObject
{
    [Tooltip("If true, enables debug logging for the package.")]
    public bool enableDebugLogging = false;
}