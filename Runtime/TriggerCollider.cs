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

            if (TryGetComponent<Collider>(out Collider collider))
            {
                collider.isTrigger = true; //Ensure that the collider is set to be a trigger
                return;
            }

            PackageLogger.Log($"No Collider component found on {gameObject.name}. Attempting to add default Collider...");

            //If there is no Collider component, check for a Collider2D component. 
            if (TryGetComponent<Collider2D>(out Collider2D collider2D))
            {
                PackageLogger.LogWarning($"A <color=lime>Collider2D</color> component was found on {gameObject.name}! <color=yellow>Cannot automatically add a defaul Collider. Please remove the <color=lime>Collider2D</color> and manually add a Collider.</color>");
                return;
            }

            //Load Package Settings
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings");

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultColliderType())
            {
                case (ColliderType.Box):
                    PackageLogger.Log("Added a <color=lime>BoxCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<BoxCollider>().isTrigger = true;
                    break;
                case (ColliderType.Sphere):
                    PackageLogger.Log("Added a <color=lime>SphereCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<SphereCollider>().isTrigger = true;
                    break;
                case (ColliderType.Capsule):
                    PackageLogger.Log("Added a <color=lime>CapsuleCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
                    break;
                case (ColliderType.Mesh):
                    PackageLogger.Log("Added a <color=lime>MeshCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<MeshCollider>().isTrigger = true;
                    break;
            }
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
