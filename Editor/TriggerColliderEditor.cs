#if HAS_HEALTH_SYSTEM
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerCollider))]
public class TriggerColliderEditor : Editor
{
    SerializedProperty onTriggerEnterCollider;
    SerializedProperty onTriggerStayCollider;
    SerializedProperty onTriggerExitCollider;

    SerializedProperty onTriggerEnterColliderInt;
    SerializedProperty onTriggerStayColliderInt;
    SerializedProperty onTriggerExitColliderInt;

    SerializedProperty onEnterType;
    SerializedProperty onStayType;
    SerializedProperty onExitType;

    SerializedProperty damageAmount;
    
    //Get references to SerializedProperties
    void OnEnable()
    {
        onTriggerEnterCollider = serializedObject.FindProperty("onTriggerEnterCollider");
        onTriggerStayCollider = serializedObject.FindProperty("onTriggerStayCollider");
        onTriggerExitCollider = serializedObject.FindProperty("onTriggerExitCollider");

        onTriggerEnterColliderInt = serializedObject.FindProperty("onTriggerEnterColliderInt");
        onTriggerStayColliderInt = serializedObject.FindProperty("onTriggerStayColliderInt");
        onTriggerExitColliderInt = serializedObject.FindProperty("onTriggerExitColliderInt");

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

        //--- OnTriggerEnter Section ---
        EditorGUILayout.PropertyField(onEnterType, new GUIContent("Event Type", "Determines which UnityEvent(s) are invoked when a Collider enters the trigger."));

        //Determine which UnityEvents to show based on selected EventType
        switch (onEnterType.enumValueIndex)
        {
            case (0): // Collider
                EditorGUILayout.PropertyField(onTriggerEnterCollider, new GUIContent("OnTriggerEnter", "Event triggered when a Collider enters the trigger. Functions called in order from top to bottom."));
                break;
            case (1): // ColliderInt
                EditorGUILayout.PropertyField(onTriggerEnterColliderInt, new GUIContent("OnTriggerEnter", "Event triggered when a Collider enters the trigger. Functions called in order from top to bottom."));
                break;
            case (2): // Both
                EditorGUILayout.PropertyField(onTriggerEnterCollider, new GUIContent("OnTriggerEnter", "Event triggered when a Collider enters the trigger. Functions called in order from top to bottom. This event's functions are invoked BEFORE the <Collider,Int,GameObject> event's functions."));
                EditorGUILayout.PropertyField(onTriggerEnterColliderInt, new GUIContent("OnTriggerEnter", "Event triggered when a Collider enters the trigger. Functions called in order from top to bottom. This event's functions are invoked AFTER the <Collider,GameObject> event's functions."));
                break;
        }

        EditorGUILayout.Space();

        //--- OnTriggerStay Section ---
        EditorGUILayout.PropertyField(onStayType, new GUIContent("Event Type", "Determines which UnityEvent(s) are invoked when a Collider stays within the trigger."));

        //Determine which UnityEvents to show based on selected EventType
        switch (onStayType.enumValueIndex)
        {
            case (0): // Collider
                EditorGUILayout.PropertyField(onTriggerStayCollider, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider that remains within the trigger. Functions called in order from top to bottom."));
                break;
            case (1): // ColliderInt
                EditorGUILayout.PropertyField(onTriggerStayColliderInt, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider that remains within the trigger. Functions called in order from top to bottom."));
                break;
            case (2): // Both
                EditorGUILayout.PropertyField(onTriggerStayCollider, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider that remains within the trigger. Functions called in order from top to bottom. This event's functions are invoked BEFORE the <Collider,Int,GameObject> event's functions."));
                EditorGUILayout.PropertyField(onTriggerStayColliderInt, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider that remains within the trigger. Functions called in order from top to bottom. This event's functions are invoked AFTER the <Collider,GameObject> event's functions."));
                break;
        }

        EditorGUILayout.Space();

        //--- OnTriggerExit Section ---
        EditorGUILayout.PropertyField(onExitType, new GUIContent("Event Type", "Determines which UnityEvent(s) are invoked when a Collider exits the trigger."));

        //Determine which UnityEvents to show based on selected EventType
        switch (onExitType.enumValueIndex) 
        {
            case (0): // Collider
                EditorGUILayout.PropertyField(onTriggerExitCollider, new GUIContent("OnTriggerExit", "Event triggered when a Collider exits the trigger. Functions called in order from top to bottom."));
                break;
            case(1): // ColliderInt
                EditorGUILayout.PropertyField(onTriggerExitColliderInt, new GUIContent("OnTriggerExit", "Event triggered when a Collider exits the trigger. Functions called in order from top to bottom."));
                break;
            case(2): // Both
                EditorGUILayout.PropertyField(onTriggerExitCollider, new GUIContent("OnTriggerExit", "Event triggered when a Collider exits the trigger. Functions called in order from top to bottom. This event's functions are invoked BEFORE the <Collider,Int,GameObject> event's functions."));
                EditorGUILayout.PropertyField(onTriggerExitColliderInt, new GUIContent("OnTriggerExit", "Event triggered when a Collider exits the trigger. Functions called in order from top to bottom. This event's functions are invoked AFTER the <Collider,GameObject> event's functions."));
                break;
        }

        EditorGUILayout.Space();

        //--- Simple Health System Section ---
        EditorGUILayout.LabelField("Simple Health System", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(damageAmount, new GUIContent("Damage Amount", "Amount of damage to apply when using the <Collider, Int, GameObject> events."));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
