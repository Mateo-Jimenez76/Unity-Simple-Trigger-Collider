using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using ColliderType = SimpleTriggerCollider.Editor.CustomSettings.ColliderType;
using SimpleTriggerCollider.Editor;
#endif
namespace SimpleTriggerCollider.Runtime
{
    public class TriggerCollider : MonoBehaviour
    {
        // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
        // This can be useful for debugging especially when multiple triggers are in a scene
        [SerializeField] private UnityEvent<Collider, GameObject> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider, GameObject> onTriggerStay;
        [SerializeField] private UnityEvent<Collider, GameObject> onTriggerExit;
        [Tooltip("Any object with the following checked layers will not trigger the events.")]
        [SerializeField] private LayerMask ignoreLayers;
        
#if UNITY_EDITOR
        private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

        private List<Collider> colliderList = new();
        private void _OnValidate()
        {
            if (this == null)
            {
                return;
            }

            GetComponents<Collider>(colliderList);

            if (colliderList.Count > 0)
            {
                foreach (Collider currentCollider in colliderList)
                {
                    if (currentCollider.isTrigger)
                    {
                        return;
                    }
                    continue;
                }
                Debug.LogError($"Multiple <color=lime>{nameof(Collider)}</color> components were found on <color=cyan>{gameObject.name}</color>. <color=yellow>To prevent unintended behavior this script</color> will not automatically assign one as a trigger, <color=red>please do so manually.</color>");
                return;
            }
            else
            {
                PackageLogger.Log($"No <color=lime>{nameof(Collider)}</color> component found on {gameObject.name}. Attempting to add default <color=lime>{nameof(Collider)}...</color>");
            }

            if (TryGetComponent<Collider2D>(out Collider2D collider))
            {
                PackageLogger.LogError($"A <color=lime>Collider2D</color> component was found on {gameObject.name}. {nameof(TriggerCollider)} is designed to work with <color=lime>{nameof(Collider)}</color> components only, and this issue cannot be resolved automatically. Please manually remove the <color=lime>{nameof(Collider2D)}</color> and replace it with a <color=lime>{nameof(Collider)}</color>.");
                return;
            }

            //Load Package Settings
            var settings = Resources.Load<CustomSettings>("SimpleTriggerColliderSettings");

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultColliderType())
            {
                case (ColliderType.Box):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>BoxCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<BoxCollider>().isTrigger = true;
                    break;
                case (ColliderType.Sphere):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>SphereCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<SphereCollider>().isTrigger = true;
                    break;
                case (ColliderType.Capsule):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>CapsuleCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
                    break;
                case (ColliderType.Mesh):
                    PackageLogger.Log("<color=yellow>Added</color> a <color=lime>MeshCollider</color> component because <color=lime>TriggerCollider.cs</color> depends on a <color=lime>Collider</color> component being present. You can change this behavior in the package's settings.");
                    gameObject.AddComponent<MeshCollider>().isTrigger = true;
                    break;
            }
        }
#endif
        private void OnTriggerEnter(Collider collision)
        {
            if (CollidedWithSelf(collision))
            {
                return;
            }
            if ((ignoreLayers.value & (1 << collision.gameObject.layer)) != 0)
            {
                return;
            }
            onTriggerEnter.Invoke(collision, gameObject);
        }

        private void OnTriggerStay(Collider collision)
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

        private void OnTriggerExit(Collider collision)
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

        private bool CollidedWithSelf(Collider collision)
        {
            return collision.gameObject == gameObject;
        }
    }
}
