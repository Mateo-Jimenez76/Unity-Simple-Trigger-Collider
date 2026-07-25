using UnityEngine;
using UnityEngine.Events;
using SimpleTriggerCollider.Editor;
using System.Collections.Generic;
#if UNITY_EDITOR
using Collider2DType = SimpleTriggerCollider.Editor.CustomSettings.Collider2DType;
#endif
namespace SimpleTriggerCollider.Runtime
{
    [AddComponentMenu("Physics 2D/Trigger Collider 2D")]
    public class TriggerCollider2D : MonoBehaviour
    {
        // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
        // This can be useful for debugging especially when multiple triggers are in a scene
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerEnter;
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerStay;
        [SerializeField] private UnityEvent<Collider2D, GameObject> onTriggerExit;
        //[SerializeField] private UnityTriggerEvent2D ;
        [Tooltip("Any object with the following checked layers will not trigger the events.")]
        [SerializeField] private LayerMask ignoreLayers;

        private new bool enabled = true;
#if UNITY_EDITOR
        private List<Collider2D> collider2DList = new();
        private void Reset()
        {
            if (IsTriggerPresent())
            {
                return;
            }
            Debug.Log($"No trigger <color=lime>{nameof(Collider2D)}</color> component found on <color=cyan>{gameObject.name}</color>. <color=yellow>Attempting to add</color> a default <color=lime>{nameof(Collider2D)}</color>.", this);
            AutoAddCollider();
        }

        private void AutoAddCollider()
        {
            //Load Package Settings
            var settings = Resources.Load<CustomSettings>(CustomSettings.settingsResourcePath);

            //Check for what kind of Collider2D to create
            switch (settings.GetDefaultCollider2DType())
            {
                case (Collider2DType.Box):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(BoxCollider2D)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider2D)}</color> depends on a <color=lime>{nameof(Collider2D)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Circle):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(CircleCollider2D)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider2D)}</color> depends on a <color=lime>{nameof(Collider2D)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Polygon):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(PolygonCollider2D)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider2D)}</color> depends on a <color=lime>{nameof(Collider2D)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
                    break;
                case (Collider2DType.Edge):
                    Debug.Log($"<color=yellow>Added</color> a <color=lime>{nameof(EdgeCollider2D)}</color> component to <color=cyan>{gameObject.name}</color> because <color=lime>{nameof(TriggerCollider2D)}</color> depends on a <color=lime>{nameof(Collider2D)}</color> component being present. You can change this behavior in the package's settings.", this);
                    gameObject.AddComponent<EdgeCollider2D>().isTrigger = true;
                    break;
            }
        }

        private bool IsTriggerPresent()
        {
            if (TryGetComponent<Collider>(out Collider collider))
            {
                Debug.LogError($"<color=lime>{nameof(TriggerCollider2D)}</color> cannot automatically fix the collider setup on <color=cyan>{gameObject.name}</color>, because <color=red>a {nameof(Collider)} component was found and {nameof(TriggerCollider2D)} only supports {nameof(Collider2D)} components</color>. Manually remove the <color=lime>{nameof(Collider)}</color> component and replace it with a <color=lime>{nameof(Collider2D)}</color>.", this);
                return false;
            }

            GetComponents(collider2DList);

            if (collider2DList.Count > 0)
            {
                foreach (Collider2D currentCollider in collider2DList)
                {
                    if (currentCollider.isTrigger)
                    {
                        return true;
                    }
                    continue;
                }
                Debug.LogError($"<color=lime>{nameof(TriggerCollider2D)}</color> did not automatically assign a trigger on <color=cyan>{gameObject.name}</color>, because multiple <color=lime>{nameof(Collider2D)}</color> components were found and <color=red>none is set as a trigger</color>. Manually set <color=cyan>isTrigger</color> to true on the intended <color=lime>{nameof(Collider2D)}</color>.", this);
                return false;
            }

            return true;
        }
#endif

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!CanCollide(collision))
            {
                return;
            }
            onTriggerEnter.Invoke(collision, gameObject);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!CanCollide(collision))
            {
                return;
            }
            onTriggerStay.Invoke(collision, gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!CanCollide(collision))
            {
                return;
            }
            onTriggerExit.Invoke(collision, gameObject);
        }

        private bool CanCollide(Collider2D collision)
        {
            if (!enabled)
            {
                return false;
            }
            if (CollidedWithSelf(collision))
            {
                return false;
            }
            if ((ignoreLayers.value & (1 << collision.gameObject.layer)) != 0)
            {
                return false;
            }
            return true;
        }

        private bool CollidedWithSelf(Collider2D collision)
        {
            return collision.gameObject == gameObject;
        }

        public void SetActive(bool value)
        {
            enabled = value;
        }
    }
}