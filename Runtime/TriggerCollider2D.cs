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
            if(this == null)
            {
                return;
            }

            if(TryGetComponent<Collider>(out Collider collider))
            {
                PackageLogger.LogError("A Collider component was found on " + gameObject.name + ". TriggerCollider2D.cs is designed to work with Collider2D components only, and this issue cannot be resolved automatically. Please manually remove the Collider and replace it with a Collider2D.");
                return;
            }

            //Check if a collider2D exists on the game object.
            if (TryGetComponent<Collider2D>(out Collider2D collider2D))
            {
                collider2D.isTrigger = true; //Ensure that the collider is set to be a trigger
                return;
            }

            //Load Package Settings
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings"); 

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultCollider2DType())
            {
                case (Collider2DType.Box):
                    PackageLogger.Log("Added a BoxCollider2D component because TriggerCollider2D.cs depends on a Collider2D component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Circle):
                    PackageLogger.Log("Added a CircleCollider2D component because TriggerCollider2D.cs depends on a Collider2D component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Polygon):
                    PackageLogger.Log("Added a PolygonCollider2D component because TriggerCollider2D.cs depends on a Collider2D component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Edge):
                    PackageLogger.Log("Added a EdgeCollider2D component because TriggerCollider2D.cs depends on a Collider2D component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<EdgeCollider2D>().isTrigger = true;
                    break;
            }
            Debug.Log("No Collider2D component was found on " + gameObject.name + " so one was added automatically. This is required for TriggerCollider2D to work. You can change this behavior in the package's settings.");
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

