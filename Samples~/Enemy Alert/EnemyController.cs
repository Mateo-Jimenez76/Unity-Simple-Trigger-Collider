using UnityEngine;
namespace SimpleTriggerCollider.Demo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        private bool isAgro = false;
        private Rigidbody2D rb;
        private Transform target;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            target = FindAnyObjectByType<PlayerMovement>().transform;
        }

        public void BecomeAgro()
        {
            isAgro = true;
        }

        private void Update()
        {
            if (isAgro)
            {
                // Move towards the target
                Vector2 direction = (target.position - transform.position).normalized;
                direction.y = 0; // Keep the enemy on ground
                rb.linearVelocity = direction * speed;
            }
        }
    }
}
