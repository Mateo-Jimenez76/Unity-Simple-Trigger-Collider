using UnityEngine;
namespace SimpleTriggerCollider.Runtime
{
    /// <summary>
    /// This class is used to store information about an object that should be instantiated when a trigger event occurs. 
    /// It allows you to specify the object to instantiate and the location where it should be instantiated, 
    /// which can be either a specific <see cref="Transform"/>, a <see cref="Vector3"/>, or the location of the <see cref="Collision"/>(2D) that triggered the event.
    /// </summary>
    public class InstantiationInfo : MonoBehaviour
    {
        [SerializeField] private GameObject objectToInstantiate;
        [SerializeField] private LocationType locationType;
        [SerializeField] private Vector3 locationVector3;
        [SerializeField] private Transform locationTransform;

        public GameObject ObjectToInstantiate => objectToInstantiate;
        public LocationType _LocationType => locationType;
        public Vector3 Location => locationVector3;
        public Transform LocationTransform => locationTransform;

        public enum LocationType
        {
            Transform,
            Vector3,
            /// <summary>
            /// The location of the collision that triggered the event.
            /// </summary>
            Collision,
        }
    }
}
