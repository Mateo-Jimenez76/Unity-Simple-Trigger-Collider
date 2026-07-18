using UnityEditor;
using SimpleTriggerCollider.Runtime;
namespace SimpleTriggerCollider.Editor
{
    [CustomEditor(typeof(InstantiationInfo))]
    public class InstantiationInfoEditor : UnityEditor.Editor
    {
        private SerializedProperty objectToInstantiateProp;
        private SerializedProperty locationTypeProp;
        private SerializedProperty locationProp;
        private SerializedProperty locationTransformProp;

        private void OnEnable()
        {
            // Link the serialized properties to the actual fields in the class
            objectToInstantiateProp = serializedObject.FindProperty("objectToInstantiate");
            locationTypeProp = serializedObject.FindProperty("locationType");
            locationProp = serializedObject.FindProperty("locationVector3");
            locationTransformProp = serializedObject.FindProperty("locationTransform");
        }

        public override void OnInspectorGUI()
        {
            // Always call this at the beginning of OnInspectorGUI
            serializedObject.Update();

            if(objectToInstantiateProp.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("No object assigned to instantiate. Please assign a GameObject.", MessageType.Warning);
            }
            // Draw the object to instantiate field like normal
            EditorGUILayout.PropertyField(objectToInstantiateProp);

            // Draw the location type enum dropdown
            EditorGUILayout.PropertyField(locationTypeProp);

            // Check if the current selected enum value matches Vector3
            // (Cast to the enum type to make it clean and readable)
            InstantiationInfo.LocationType currentType = (InstantiationInfo.LocationType)locationTypeProp.enumValueIndex;

            if (currentType == InstantiationInfo.LocationType.Vector3)
            {
                EditorGUILayout.HelpBox("The location is set to a Vector3. Please enter the desired coordinates.", MessageType.Info);
                // Indent the field slightly so it looks organized under the dropdown
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(locationProp);
                EditorGUI.indentLevel--;
            }

            if(currentType == InstantiationInfo.LocationType.Transform)
            {
                EditorGUILayout.HelpBox("The location is set to a Transform. Please assign the desired Transform.", MessageType.Info);
                // Indent the field slightly so it looks organized under the dropdown
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(locationTransformProp);
                EditorGUI.indentLevel--;
            }

            if(currentType == InstantiationInfo.LocationType.Collision)
            {
                EditorGUILayout.HelpBox("The location is set to Collision. The object will be instantiated at point of collision", MessageType.Info);
            }

            // Apply any changes made in the inspector to the actual object
            serializedObject.ApplyModifiedProperties();
        }
    }
}