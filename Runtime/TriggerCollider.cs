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
                Debug.LogError($"<color=lime>{nameof(TriggerCollider)}</color> did not automatically assign a trigger on <color=cyan>{gameObject.name}</color>, because multiple <color=lime>{nameof(Collider)}</color> components were found and <color=red>none is set as a trigger</color>. Manually set <color=cyan>isTrigger</color> to true on the intended <color=lime>{nameof(Collider)}</color>.", this);
                return;
            }
            else
            {
                Debug.Log($"No <color=lime>{nameof(Collider)}</color> component found on <color=cyan>{gameObject.name}</color>. <color=yellow>Attempting to add</color> a default <color=lime>{nameof(Collider)}</color>.", this);
            }

            if (TryGetComponent<Collider2D>(out Collider2D collider))
            {
                Debug.LogError($"<color=lime>{nameof(TriggerCollider)}</color> cannot automatically fix the collider setup on <color=cyan>{gameObject.name}</color>, because <color=red>a {nameof(Collider2D)} component was found and {nameof(TriggerCollider)} only supports {nameof(Collider)} components</color>. Manually remove the <color=lime>{nameof(Collider2D)}</color> component and replace it with a <color=lime>{nameof(Collider)}</color>.", this);
                return;
            }

            //Load Package Settings
            var settings = Resources.Load<CustomSettings>(CustomSettings.settingsResourcePath);

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultColliderType())
            {
                case (ColliderType.Box):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(BoxCollider)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider)}</color> depends on a <color=lime>{nameof(Collider)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<BoxCollider>().isTrigger = true;
                    break;
                case (ColliderType.Sphere):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(SphereCollider)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider)}</color> depends on a <color=lime>{nameof(Collider)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<SphereCollider>().isTrigger = true;
                    break;
                case (ColliderType.Capsule):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(CapsuleCollider)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider)}</color> depends on a <color=lime>{nameof(Collider)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
                    break;
                case (ColliderType.Mesh):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(MeshCollider)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider)}</color> depends on a <color=lime>{nameof(Collider)}</color> component being present. You can change this behavior in the package's settings.", this);
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