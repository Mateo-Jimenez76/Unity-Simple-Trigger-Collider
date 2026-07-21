using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityTriggerEvent2D
{
    public UnityEvent<Collider2D, GameObject> onTriggerEnter = new();
    public UnityEvent<Collider2D, GameObject> onTriggerStay = new();
    public UnityEvent<Collider2D, GameObject> onTriggerExit = new();
}
