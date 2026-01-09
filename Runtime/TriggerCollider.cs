using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if !HAS_HEALTH_SYSTEM
[RequireComponent(typeof(Collider))]
public class TriggerCollider : MonoBehaviour
{
    // The GameObject argument is used to pass the caller object(the object this script is attached to) to the dynamic functions
    // This can be useful for debugging especially when multiple triggers are in a scene
    [SerializeField] private UnityEvent<Collider,GameObject> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider,GameObject> onTriggerStay;
    [SerializeField] private UnityEvent<Collider,GameObject> onTriggerExit;
    
    private void OnTriggerEnter(Collider collision)
    {
        onTriggerEnter.Invoke(collision, gameObject);
    }

    private void OnTriggerStay(Collider collision)
    {
        onTriggerStay.Invoke(collision, gameObject);
    }

    private void OnTriggerExit(Collider collision)
    {
        onTriggerExit.Invoke(collision, gameObject);
    }
}
#endif

#if HAS_HEALTH_SYSTEM
[RequireComponent(typeof(Collider))]
public class TriggerCollider : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider,GameObject> onTriggerEnterCollider;
    [SerializeField] private UnityEvent<Collider,GameObject> onTriggerStayCollider;
    [SerializeField] private UnityEvent<Collider,GameObject> onTriggerExitCollider;

    //The int parameter is intended to be used for passing the damage amount variable to health system functions
    [SerializeField] private UnityEvent<Collider,int,GameObject> onTriggerEnterColliderInt;
    [SerializeField] private UnityEvent<Collider,int,GameObject> onTriggerStayColliderInt;
    [SerializeField] private UnityEvent<Collider,int,GameObject> onTriggerExitColliderInt;

    [SerializeField] private EventType onEnterType = EventType.Collider;
    [SerializeField] private EventType onStayType = EventType.Collider;
    [SerializeField] private EventType onExitType = EventType.Collider;


    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter(Collider collision)
    {
        switch (onEnterType)
        {
            case (EventType.Collider):
                onTriggerEnterCollider.Invoke(collision, gameObject);
                break;
            case (EventType.ColliderInt):
                onTriggerEnterColliderInt.Invoke(collision, damageAmount, gameObject);
                break;
            case (EventType.Both):
                onTriggerEnterCollider.Invoke(collision, gameObject);
                onTriggerEnterColliderInt.Invoke(collision, damageAmount, gameObject);
                break;
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        switch (onStayType)
        {
            case (EventType.Collider):
                onTriggerStayCollider.Invoke(collision, gameObject);
                break;
            case (EventType.ColliderInt):
                onTriggerStayColliderInt.Invoke(collision, damageAmount, gameObject);
                break;
            case (EventType.Both):
                onTriggerStayCollider.Invoke(collision, gameObject);
                onTriggerStayColliderInt.Invoke(collision, damageAmount, gameObject);
                break;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        switch (onExitType)
        {
            case (EventType.Collider):
                onTriggerExitCollider.Invoke(collision, gameObject);
                break;
            case (EventType.ColliderInt):
                onTriggerExitColliderInt.Invoke(collision, damageAmount, gameObject);
                break;
            case (EventType.Both):
                onTriggerExitCollider.Invoke(collision, gameObject);
                onTriggerExitColliderInt.Invoke(collision, damageAmount, gameObject);
                break;
        }
    }

    /// <summary>
    /// Types of events for TriggerCollider. 
    /// Used to determine which UnityEvents to invoke at runtime and which to show in the Inspector.
    /// </summary>
    private enum EventType 
    {
        Collider,
        ColliderInt,
        Both
    }
}
#endif

