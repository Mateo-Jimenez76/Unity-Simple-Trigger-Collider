#if HAS_HEALTH_SYSTEM
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerCollider2D))]
public class TriggerCollider2DEditor : Editor
{
    SerializedProperty onTriggerEnterCollider2D;
    SerializedProperty onTriggerStayCollider2D;
    SerializedProperty onTriggerExitCollider2D;

    SerializedProperty onTriggerEnterCollider2DInt;
    SerializedProperty onTriggerStayCollider2DInt;
    SerializedProperty onTriggerExitCollider2DInt;

    SerializedProperty onEnterType;
    SerializedProperty onStayType;
    SerializedProperty onExitType;

    SerializedProperty damageAmount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        onTriggerEnterCollider2D = serializedObject.FindProperty("onTriggerEnterCollider2D");
        onTriggerStayCollider2D = serializedObject.FindProperty("onTriggerStayCollider2D");
        onTriggerExitCollider2D = serializedObject.FindProperty("onTriggerExitCollider2D");

        onTriggerEnterCollider2DInt = serializedObject.FindProperty("onTriggerEnterCollider2DInt");
        onTriggerStayCollider2DInt = serializedObject.FindProperty("onTriggerStayCollider2DInt");
        onTriggerExitCollider2DInt = serializedObject.FindProperty("onTriggerExitCollider2DInt");

        onEnterType = serializedObject.FindProperty("onEnterType");
        onStayType = serializedObject.FindProperty("onStayType");
        onExitType = serializedObject.FindProperty("onExitType");

        damageAmount  = serializedObject.FindProperty("damageAmount");
    }

    public override void OnInspectorGUI() 
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(onEnterType);
        switch (onEnterType.enumValueIndex)
        {
            case (0):
                EditorGUILayout.PropertyField(onTriggerEnterCollider2D, new GUIContent("OnTriggerEnter"));
                break;
            case (1):
                EditorGUILayout.PropertyField(onTriggerEnterCollider2DInt, new GUIContent("OnTriggerEnter"));
                break;
            case (2):
                EditorGUILayout.PropertyField(onTriggerEnterCollider2D, new GUIContent("OnTriggerEnter"));
                EditorGUILayout.PropertyField(onTriggerEnterCollider2DInt, new GUIContent("OnTriggerEnter"));
                break;
        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(onStayType);
        switch (onStayType.enumValueIndex)
        {
            case (0):
                EditorGUILayout.PropertyField(onTriggerStayCollider2D, new GUIContent("OnTriggerStay"));
                break;
            case (1):
                EditorGUILayout.PropertyField(onTriggerStayCollider2DInt, new GUIContent("OnTriggerStay"));
                break;
            case (2):
                EditorGUILayout.PropertyField(onTriggerStayCollider2D, new GUIContent("OnTriggerStay"));
                EditorGUILayout.PropertyField(onTriggerStayCollider2DInt, new GUIContent("OnTriggerStay"));
                break;
        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(onExitType);
        switch (onExitType.enumValueIndex) 
        {
            case (0):
                EditorGUILayout.PropertyField(onTriggerExitCollider2D, new GUIContent("OnTriggerExit"));
                break;
            case(1):
                EditorGUILayout.PropertyField(onTriggerExitCollider2DInt, new GUIContent("OnTriggerExit"));
                break;
            case(2):
                EditorGUILayout.PropertyField(onTriggerExitCollider2D, new GUIContent("OnTriggerExit"));
                EditorGUILayout.PropertyField(onTriggerExitCollider2DInt, new GUIContent("OnTriggerExit"));
                break;
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Simple Health System", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(damageAmount);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
