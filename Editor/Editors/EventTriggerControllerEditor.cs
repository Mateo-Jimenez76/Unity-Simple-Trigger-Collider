using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using SimpleTriggerCollider.Runtime;

[CustomEditor(typeof(TriggerCollider2D))]
public class EventTriggerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector fields (like ignoreLayers and triggerEvents)
        DrawDefaultInspector();

        // Inspect onTriggerEnter (or stay/exit) inside triggerEvents
        SerializedProperty onTriggerEnterProp = serializedObject.FindProperty("onTriggerEnter");

        if (onTriggerEnterProp != null)
        {
            CheckCalls(onTriggerEnterProp);
        }
    }

    private void CheckCalls(SerializedProperty eventProp)
    {
        SerializedProperty callsProp = eventProp.FindPropertyRelative("m_PersistentCalls.m_Calls");
        if (callsProp == null) return;

        for (int i = 0; i < callsProp.arraySize; i++)
        {
            SerializedProperty call = callsProp.GetArrayElementAtIndex(i);
            string methodName = call.FindPropertyRelative("m_MethodName").stringValue;
            UnityEngine.Object targetObj = call.FindPropertyRelative("m_Target").objectReferenceValue;

            if (targetObj != null && !string.IsNullOrEmpty(methodName))
            {
                CheckAndRenderComponentRequirement(targetObj, methodName);
            }
        }
    }

    private void CheckAndRenderComponentRequirement(UnityEngine.Object targetObj, string methodName)
    {
        Type targetType = targetObj.GetType();

        // 1. Fetch all methods on the target matching the name and visibility flags
        MethodInfo[] methods = targetType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        // 2. Iterate through all overloads matching methodName
        foreach (MethodInfo methodInfo in methods)
        {
            if (methodInfo.Name != methodName) continue;

            // Check if this specific overload has your custom attribute
            var attribute = methodInfo.GetCustomAttribute<RequiresInfoComponentAttribute>();
            if (attribute != null)
            {
                Type requiredType = attribute.RequiredComponentType;

                GameObject targetGO = ((TriggerCollider2D)target).gameObject;

                if (targetGO != null && targetGO.GetComponent(requiredType) == null)
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.HelpBox(
                        $"Function '{methodName}' requires the '{requiredType.Name}' component on '{targetGO.name}'.",
                        MessageType.Warning);

                    if (GUILayout.Button($"Add {requiredType.Name} to {targetGO.name}"))
                    {
                        Undo.AddComponent(targetGO, requiredType);
                        EditorUtility.SetDirty(targetGO);
                    }
                    EditorGUILayout.EndVertical();
                }
                else if (targetGO != null)
                {
                    EditorGUILayout.HelpBox($"✓ '{methodName}' is linked to parameters in '{requiredType.Name}'.", MessageType.Info);
                }

                // Found a matching attribute overload; break unless you allow multiple attributes across overloads
                break;
            }
        }
    }
}
