using UnityEngine;
using UnityEngine.Events;
using SimpleTriggerCollider.Editor;
using ColliderType = SimpleTriggerCollider.Editor.CustomSettings.ColliderType;
namespace SimpleTriggerCollider.Runtime
{
    public class TriggerCollider : MonoBehaviour
    {
        // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
        // This can be useful for debugging especially when multiple triggers are in a scene
        [SerializeField] private UnityEvent<Collider, GameObject> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider, GameObject> onTriggerStay;
        [SerializeField] private UnityEvent<Collider, GameObject> onTriggerExit;

        private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

        private void _OnValidate()
        {
            if (this == null)
            {
                return;
            }

            //Check if a collider2D exists on the game object.
            if (TryGetComponent<Collider>(out Collider collider))
            {
                collider.isTrigger = true; //Ensure that the collider is set to be a trigger
                return;
            }

            //Load Package Settings
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings");

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultColliderType())
            {
                case (ColliderType.Box):
                    PackageLogger.Log("Added a BoxCollider component because TriggerCollider.cs depends on a Collider component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<BoxCollider>().isTrigger = true;
                    break;
                case (ColliderType.Sphere):
                    PackageLogger.Log("Added a SphereCollider component because TriggerCollider.cs depends on a Collider component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<SphereCollider>().isTrigger = true;
                    break;
                case (ColliderType.Capsule):
                    PackageLogger.Log("Added a CapsuleCollider component because TriggerCollider.cs depends on a Collider component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
                    break;
                case (ColliderType.Mesh):
                    PackageLogger.Log("Added a MeshCollider component because TriggerCollider.cs depends on a Collider component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<MeshCollider>().isTrigger = true;
                    break;
            }
            Debug.Log("No Collider component was found on " + gameObject.name + " so one was added automatically. This is required for TriggerCollider to work. You can change this behavior in the package's settings.");
        }

        private void OnTriggerEnter(Collider collision)
        {
            onTriggerEnter.Invoke(collision, gameObject);
        }

        private void OnTriggerStay(Collider collision)
        {
            onTriggerStay.Invoke(collision, gameObject);
        }

        private void OnTriggerExit(Collider collision)
        {
            onTriggerExit.Invoke(collision, gameObject);
        }
    }
}
