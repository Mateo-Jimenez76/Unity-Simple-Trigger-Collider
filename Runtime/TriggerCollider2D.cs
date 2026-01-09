using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if !HAS_HEALTH_SYSTEM
[RequireComponent(typeof(Collider2D))]
public class TriggerCollider2D : MonoBehaviour
{
    // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
    // This can be useful for debugging especially when multiple triggers are in a scene
    [SerializeField] private UnityEvent<Collider2D,GameObject> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider2D,GameObject> onTriggerStay;
    [SerializeField] private UnityEvent<Collider2D,GameObject> onTriggerExit;

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
#endif

#if HAS_HEALTH_SYSTEM
[RequireComponent(typeof(Collider2D))]
public class TriggerCollider2D : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D,GameObject> onTriggerEnterCollider2D;
    [SerializeField] private UnityEvent<Collider2D,GameObject> onTriggerStayCollider2D;
    [SerializeField] private UnityEvent<Collider2D,GameObject> onTriggerExitCollider2D;

    //The int parameter is intended to be used for passing the damage amount variable to health system functions
    [SerializeField] private UnityEvent<Collider2D,int,GameObject> onTriggerEnterCollider2DInt;
    [SerializeField] private UnityEvent<Collider2D,int,GameObject> onTriggerStayCollider2DInt;
    [SerializeField] private UnityEvent<Collider2D,int,GameObject> onTriggerExitCollider2DInt;

    [SerializeField] private EventType onEnterType = EventType.Collider2D;
    [SerializeField] private EventType onStayType = EventType.Collider2D;
    [SerializeField] private EventType onExitType = EventType.Collider2D;


    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (onEnterType)
        {
            case (EventType.Collider2D):
                onTriggerEnterCollider2D.Invoke(collision, gameObject);
                break;
            case (EventType.Collider2DInt):
                onTriggerEnterCollider2DInt.Invoke(collision, damageAmount, gameObject);
                break;
            case (EventType.Both):
                onTriggerEnterCollider2D.Invoke(collision, gameObject);
                onTriggerEnterCollider2DInt.Invoke(collision, damageAmount, gameObject);
                break;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (onStayType)
        {
            case (EventType.Collider2D):
                onTriggerStayCollider2D.Invoke(collision, gameObject);
                break;
            case (EventType.Collider2DInt):
                onTriggerStayCollider2DInt.Invoke(collision, damageAmount, gameObject);
                break;
            case (EventType.Both):
                onTriggerStayCollider2D.Invoke(collision, gameObject);
                onTriggerStayCollider2DInt.Invoke(collision, damageAmount, gameObject);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (onExitType)
        {
            case (EventType.Collider2D):
                onTriggerExitCollider2D.Invoke(collision, gameObject);
                break;
            case (EventType.Collider2DInt):
                onTriggerExitCollider2DInt.Invoke(collision, damageAmount, gameObject);
                break;
            case (EventType.Both):
                onTriggerExitCollider2D.Invoke(collision, gameObject);
                onTriggerExitCollider2DInt.Invoke(collision, damageAmount, gameObject);
                break;
        }
    }

    /// <summary>
    /// Types of events for TriggerCollider. 
    /// Used to determine which UnityEvents to invoke at runtime and which to show in the Inspector.
    /// </summary>
    private enum EventType 
    {
        Collider2D,
        Collider2DInt,
        Both
    }
}
#endif

