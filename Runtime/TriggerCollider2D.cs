using UnityEngine;
using UnityEngine.Events;
using SimpleTriggerCollider.Editor;
using Collider2DType = SimpleTriggerCollider.Editor.CustomSettings.Collider2DType;
using System.Collections.Generic;
namespace SimpleTriggerCollider.Runtime
{
    public class TriggerCollider2D : MonoBehaviour
    {
        // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
        // This can be useful for debugging especially when multiple triggers are in a scene
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerStay;
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerExit;
        [SerializeField] private LayerMask ignoreLayers;

        private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

        private List<Collider2D> collider2DList = new();
        private void _OnValidate()
        {
            if (this == null)
            {
                return;
            }
            GetComponents<Collider2D>(collider2DList);

            if (collider2DList.Count > 0)
            {
                foreach (Collider2D currentCollider in collider2DList)
                {
                    if (currentCollider.isTrigger)
                    {
                        return;
                    }
                    continue;
                }
                Debug.LogError($"Multiple <color=lime>Collider2D</color> components were found on <color=cyan>{gameObject.name}</color>. <color=yellow>To prevent unintended behavior this script</color> will not automatically assign one as a trigger, <color=red>please do so manually.</color>");
                return;
            }
            else
            {
                PackageLogger.Log($"No <color=lime>Collider2D</color> component found on {gameObject.name}. Attempting to add default <color=lime>Collider2D...");
            }

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
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>BoxCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Circle):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>CircleCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Polygon):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>PolygonCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Edge):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>EdgeCollider2D</color> component because <color=lime>TriggerCollider2D.cs</color> depends on a <color=lime>Collider2D</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<EdgeCollider2D>().isTrigger = true;
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CollidedWithSelf(collision))
            {
                return;
            }
            if((ignoreLayers.value & (1 << collision.gameObject.layer)) != 0)
            {
                return;
            }
            onTriggerEnter.Invoke(collision, gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (CollidedWithSelf(collision))
            {
                return;
            }
            if ((ignoreLayers.value & (1 << collision.gameObject.layer)) != 0)
            {
                return;
            }
            onTriggerStay.Invoke(collision, gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CollidedWithSelf(collision))
            {
                return;
            }
            if ((ignoreLayers.value & (1 << collision.gameObject.layer)) != 0)
            {
                return;
            }
            onTriggerExit.Invoke(collision, gameObject);
        }

        private bool CollidedWithSelf(Collider2D collision)
        {
            return collision.gameObject == gameObject;
        }
    }
}

