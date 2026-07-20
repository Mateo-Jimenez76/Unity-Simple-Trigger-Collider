using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class MouseCollider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
            Debug.LogError($"No camera is in the scene! This script requires a camera with the {nameof(PhysicsRaycaster)} component.");
            gameObject.SetActive(false);
            return;
        }

        if (!Camera.main.TryGetComponent(out PhysicsRaycaster raycaster))
        {
            Debug.LogWarning($"Camera '{Camera.main}' does not have the required {nameof(PhysicsRaycaster)} component, adding it now...");
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }

        if (EventSystem.current == null)
        {
            Debug.LogWarning($"No EventSystem is in the scene! This script requires an EventSystem with the {nameof(InputSystemUIInputModule)} component. Adding it now...");
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
