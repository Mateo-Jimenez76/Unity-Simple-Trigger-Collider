using UnityEngine;
namespace SimpleTriggerCollider.Demo 
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float followSpeed = 3;
        private Transform player;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            player = FindAnyObjectByType<PlayerMovement>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPosition = followSpeed * Time.deltaTime * (player.position - transform.position).normalized;
            targetPosition.z = 0;
            transform.Translate(targetPosition);
        }
    }
}

