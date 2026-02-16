using UnityEngine;
using UnityEngine.Events;
using SimpleTriggerCollider.Editor;
using Collider2DType = SimpleTriggerCollider.Editor.CustomSettings.Collider2DType;
namespace SimpleTriggerCollider.Runtime
{
    public class TriggerCollider2D : MonoBehaviour
    {
        // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
        // This can be useful for debugging especially when multiple triggers are in a scene
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerStay;
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerExit;

        private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

        private void _OnValidate()
        {
            if (this == null)
            {
                return;
            }

            //Check if a collider2D exists on the game object.
            if (TryGetComponent<Collider2D>(out Collider2D collider2D))
            {
                collider2D.isTrigger = true; //Ensure that the collider is set to be a trigger
                return;
            }

            PackageLogger.Log($"No <color=lime>Collider2D</color> component found on {gameObject.name}. Attempting to add default <color=lime>Collider2D...");

            if (TryGetComponent<Collider>(out Collider collider))
            {
                PackageLogger.LogError($"A <color=lime>Collider</color> component was found on {gameObject.name}. {nameof(TriggerCollider2D)} is designed to work with <color=lime>Collider2D</color> components only, and this issue cannot be resolved automatically. Please manually remove the <color=lime>Collider</color> and replace it with a <color=lime>Collider2D</color>.");
                return;
            }

            //Load Package Settings
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings"); 

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultCollider2DType())
            {
                case (Collider2DType.Box):
                    PackageLogger.Log("Added a <color=lime>BoxCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Circle):
                    PackageLogger.Log("Added a <color=lime>CircleCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Polygon):
                    PackageLogger.Log("Added a <color=lime>PolygonCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Edge):
                    PackageLogger.Log("Added a <color=lime>EdgeCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<EdgeCollider2D>().isTrigger = true;
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTriggerEnter.Invoke(collision, gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            onTriggerStay.Invoke(collision, gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            onTriggerExit.Invoke(collision, gameObject);
        }
    }
}

