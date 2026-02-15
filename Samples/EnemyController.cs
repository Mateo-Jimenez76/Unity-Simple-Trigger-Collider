using UnityEngine;

namespace SimpleTriggerColliders.Demo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] GameObject agroIndicator; // Optional: A visual indicator for when the enemy is agro

        private bool isAgro = false;
        private Rigidbody2D rb;
        private Transform target;
        private void Awake()
        {
            agroIndicator?.SetActive(false); // Ensure the agro indicator is off at the start

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
                agroIndicator?.SetActive(true);
                // Move towards the target
                Vector2 direction = (target.position - transform.position).normalized;
                rb.linearVelocity = direction * speed;
            }
        }
    }
}
