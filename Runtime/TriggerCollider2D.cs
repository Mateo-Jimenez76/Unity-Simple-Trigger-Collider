using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if !HAS_HEALTH_SYSTEM
[RequireComponent(typeof(Collider2D))]
public class TriggerCollider2D : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider2D> onTriggerStay;
    [SerializeField] private UnityEvent<Collider2D> onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        onTriggerStay.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke(collision);
    }
}
#endif

#if HAS_HEALTH_SYSTEM
[RequireComponent(typeof(Collider2D))]
public class TriggerCollider2D : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> onTriggerEnterCollider2D;
    [SerializeField] private UnityEvent<Collider2D> onTriggerStayCollider2D;
    [SerializeField] private UnityEvent<Collider2D> onTriggerExitCollider2D;

    [SerializeField] private UnityEvent<Collider2D,int> onTriggerEnterCollider2DInt;
    [SerializeField] private UnityEvent<Collider2D,int> onTriggerStayCollider2DInt;
    [SerializeField] private UnityEvent<Collider2D,int> onTriggerExitCollider2DInt;

    [SerializeField] private EventType onEnterType = EventType.Collider2D;
    [SerializeField] private EventType onStayType = EventType.Collider2D;
    [SerializeField] private EventType onExitType = EventType.Collider2D;


    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (onEnterType)
        {
            case (EventType.Collider2D):
                onTriggerEnterCollider2D.Invoke(collision);
                break;
            case (EventType.Collider2DAndInt):
                onTriggerEnterCollider2DInt.Invoke(collision,damageAmount);
                break;
            case (EventType.Both):
                onTriggerEnterCollider2D.Invoke(collision);
                onTriggerEnterCollider2DInt.Invoke(collision, damageAmount);
                break;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (onStayType)
        {
            case (EventType.Collider2D):
                onTriggerStayCollider2D.Invoke(collision);
                break;
            case (EventType.Collider2DAndInt):
                onTriggerStayCollider2DInt.Invoke(collision, damageAmount);
                break;
            case (EventType.Both):
                onTriggerStayCollider2D.Invoke(collision);
                onTriggerStayCollider2DInt.Invoke(collision, damageAmount);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (onExitType)
        {
            case (EventType.Collider2D):
                onTriggerExitCollider2D.Invoke(collision);
                break;
            case (EventType.Collider2DAndInt):
                onTriggerExitCollider2DInt.Invoke(collision, damageAmount);
                break;
            case (EventType.Both):
                onTriggerExitCollider2D.Invoke(collision);
                onTriggerExitCollider2DInt.Invoke(collision, damageAmount);
                break;
        }
    }

    public enum EventType 
    {
        Collider2D,
        Collider2DAndInt,
        Both
    }
}
#endif

