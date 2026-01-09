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
    
    //Get references to SerializedProperties
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

        //--- OnTriggerEnter Section ---
        EditorGUILayout.PropertyField(onEnterType, new GUIContent("Event Type", "Determines which UnityEvent(s) are invoked when a Collider2D enters the trigger."));

        //Determine which UnityEvents to show based on selected EventType
        switch (onEnterType.enumValueIndex)
        {
            case (0): // Collider2D
                EditorGUILayout.PropertyField(onTriggerEnterCollider2D, new GUIContent("OnTriggerEnter", "Event triggered when a Collider2D enters the trigger. Functions called in order from top to bottom."));
                break;
            case (1): // Collider2DInt
                EditorGUILayout.PropertyField(onTriggerEnterCollider2DInt, new GUIContent("OnTriggerEnter", "Event triggered when a Collider2D enters the trigger. Functions called in order from top to bottom."));
                break;
            case (2): // Both
                EditorGUILayout.PropertyField(onTriggerEnterCollider2D, new GUIContent("OnTriggerEnter", "Event triggered when a Collider2D enters the trigger. Functions called in order from top to bottom. This event's functions are invoked BEFORE the <Collider2D,Int,GameObject> event's functions."));
                EditorGUILayout.PropertyField(onTriggerEnterCollider2DInt, new GUIContent("OnTriggerEnter", "Event triggered when a Collider2D enters the trigger. Functions called in order from top to bottom. This event's functions are invoked AFTER the <Collider2D,GameObject> event's functions."));
                break;
        }

        EditorGUILayout.Space();

        //--- OnTriggerStay Section ---
        EditorGUILayout.PropertyField(onStayType, new GUIContent("Event Type", "Determines which UnityEvent(s) are invoked when a Collider2D stays within the trigger."));

        //Determine which UnityEvents to show based on selected EventType
        switch (onStayType.enumValueIndex)
        {
            case (0): // Collider2D
                EditorGUILayout.PropertyField(onTriggerStayCollider2D, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider2D that remains within the trigger. Functions called in order from top to bottom."));
                break;
            case (1): // Collider2DInt
                EditorGUILayout.PropertyField(onTriggerStayCollider2DInt, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider2D that remains within the trigger. Functions called in order from top to bottom."));
                break;
            case (2): // Both
                EditorGUILayout.PropertyField(onTriggerStayCollider2D, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider2D that remains within the trigger. Functions called in order from top to bottom. This event's functions are invoked BEFORE the <Collider2D,Int,GameObject> event's functions."));
                EditorGUILayout.PropertyField(onTriggerStayCollider2DInt, new GUIContent("OnTriggerStay", "Event triggered roughly every frame foreach Collider2D that remains within the trigger. Functions called in order from top to bottom. This event's functions are invoked AFTER the <Collider2D,GameObject> event's functions."));
                break;
        }

        EditorGUILayout.Space();

        //--- OnTriggerExit Section ---
        EditorGUILayout.PropertyField(onExitType, new GUIContent("Event Type", "Determines which UnityEvent(s) are invoked when a Collider2D exits the trigger."));

        //Determine which UnityEvents to show based on selected EventType
        switch (onExitType.enumValueIndex) 
        {
            case (0): // Collider2D
                EditorGUILayout.PropertyField(onTriggerExitCollider2D, new GUIContent("OnTriggerExit", "Event triggered when a Collider2D exits the trigger. Functions called in order from top to bottom."));
                break;
            case(1): // Collider2DInt
                EditorGUILayout.PropertyField(onTriggerExitCollider2DInt, new GUIContent("OnTriggerExit", "Event triggered when a Collider2D exits the trigger. Functions called in order from top to bottom."));
                break;
            case(2): // Both
                EditorGUILayout.PropertyField(onTriggerExitCollider2D, new GUIContent("OnTriggerExit", "Event triggered when a Collider2D exits the trigger. Functions called in order from top to bottom. This event's functions are invoked BEFORE the <Collider2D,Int,GameObject> event's functions."));
                EditorGUILayout.PropertyField(onTriggerExitCollider2DInt, new GUIContent("OnTriggerExit", "Event triggered when a Collider2D exits the trigger. Functions called in order from top to bottom. This event's functions are invoked AFTER the <Collider2D,GameObject> event's functions."));
                break;
        }

        EditorGUILayout.Space();

        //--- Simple Health System Section ---
        EditorGUILayout.LabelField("Simple Health System", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(damageAmount, new GUIContent("Damage Amount", "Amount of damage to apply when using the <Collider2D, Int, GameObject> events."));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
