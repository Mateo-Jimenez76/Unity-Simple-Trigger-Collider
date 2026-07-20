using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace SimpleTriggerCollider.Runtime 
{
    [RequireComponent(typeof(Collider2D))]
    public class MouseCollider2D : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private bool setupCheck = true;

        [SerializeField] private UnityEvent onMouseEnter = new();
        [SerializeField] private UnityEvent onMouseLeave = new();
        [SerializeField] private UnityEvent onClickOfCollider = new();
        public void Start()
        {
            if (!setupCheck)
            {
                return;
            }

            if (Camera.main == null)
            {
                Debug.LogError(
                    $"<color=lime>{nameof(MouseCollider2D)}</color> " +
                    $"<color=yellow>disabled</color> <color=cyan>{gameObject.name}</color>, because " +
                    $"<color=red>no camera is present in the scene</color>, and this script requires one with a " +
                    $"<color=cyan>{nameof(Physics2DRaycaster)}</color> component. Add a camera to the scene.",
                    this);
                gameObject.SetActive(false);
                return;
            }

            if (!Camera.main.TryGetComponent(out Physics2DRaycaster raycaster))
            {
                Debug.LogWarning(
                    $"<color=lime>{nameof(MouseCollider2D)}</color> " +
                    $"<color=yellow>added</color> a <color=cyan>{nameof(Physics2DRaycaster)}</color> component to " +
                    $"<color=cyan>{Camera.main.name}</color>, because <color=red>it was missing one</color>. " +
                    $"Add a {nameof(Physics2DRaycaster)} to the main camera manually to avoid this at runtime.",
                    Camera.main);
                Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
            }

            if (EventSystem.current == null)
            {
                Debug.LogWarning(
                    $"<color=lime>{nameof(MouseCollider2D)}</color> " +
                    $"<color=yellow>created</color> a new <color=cyan>EventSystem</color> with an " +
                    $"<color=cyan>{nameof(InputSystemUIInputModule)}</color>, because <color=red>no EventSystem was present in the scene</color>. " +
                    $"Add an EventSystem to the scene manually to avoid this at runtime.");
                GameObject eventSystem = new("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<InputSystemUIInputModule>();
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            onClickOfCollider?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseLeave?.Invoke();
        }
    }
}

