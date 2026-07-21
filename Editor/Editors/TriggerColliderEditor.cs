using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using SimpleTriggerCollider.Runtime;
using SimpleTriggerCollider.Runtime.CommonUseCaseFunctions;

namespace SimpleTriggerCollider.Editor
{
    [CustomEditor(typeof(TriggerCollider))]
    public class TriggerColliderEditor : UnityEditor.Editor
    {
        const string PERSISTANTCALLSPATH = "m_PersistentCalls.m_Calls";

        new GameObject target;

        SerializedProperty onTriggerEnterProp;
        SerializedProperty onTriggerStayProp;
        SerializedProperty onTriggerExitProp;

        MethodInfo[] methods;
        private void OnEnable()
        {
            onTriggerEnterProp = serializedObject.FindProperty("onTriggerEnter");
            onTriggerStayProp = serializedObject.FindProperty("onTriggerStay");
            onTriggerExitProp = serializedObject.FindProperty("onTriggerExit");

            target = ((TriggerCollider2D)base.target).gameObject;

            methods = typeof(CommonUseCaseFunctions).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (onTriggerEnterProp != null)
            {
                CheckCalls(onTriggerEnterProp);
            }

            if (onTriggerStayProp != null)
            {
                CheckCalls(onTriggerStayProp);
            }

            if (onTriggerExitProp != null)
            {
                CheckCalls(onTriggerExitProp);
            }
        }

        private void CheckCalls(SerializedProperty eventProp)
        {
            SerializedProperty callsProp = eventProp.FindPropertyRelative(PERSISTANTCALLSPATH);
            if (callsProp == null) return;

            for (int i = 0; i < callsProp.arraySize; i++)
            {
                SerializedProperty call = callsProp.GetArrayElementAtIndex(i);
                string methodName = call.FindPropertyRelative("m_MethodName").stringValue;
                UnityEngine.Object targetObj = call.FindPropertyRelative("m_Target").objectReferenceValue;

                if (targetObj != null && targetObj.GetType() != typeof(CommonUseCaseFunctions))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(methodName))
                {
                    CheckAndRenderComponentRequirement(methodName);
                }
            }
        }

        private void CheckAndRenderComponentRequirement(string methodName)
        {
            //Iterate through all overloads matching methodName
            foreach (MethodInfo methodInfo in methods)
            {
                if (methodInfo.Name != methodName) continue;

                var attribute = methodInfo.GetCustomAttribute<RequiresInfoComponentAttribute>();

                if (attribute == null)
                {
                    return;
                }

                Type requiredType = attribute.RequiredComponentType;

                if (target.GetComponent(requiredType) != null)
                {
                    EditorGUILayout.HelpBox($"✓ '{methodName}' is linked to parameters in '{requiredType.Name}'.", MessageType.Info);
                    break;
                }

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.HelpBox(
                    $"Function '{methodName}' requires the '{requiredType.Name}' component on '{target.name}'.",
                    MessageType.Warning);

                if (GUILayout.Button($"Add {requiredType.Name} to {target.name}"))
                {
                    Undo.AddComponent(target, requiredType);
                    EditorUtility.SetDirty(target);
                }
                EditorGUILayout.EndVertical();
                break;
            }
        }
    }
}
