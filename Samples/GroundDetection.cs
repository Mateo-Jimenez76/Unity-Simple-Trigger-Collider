using UnityEngine;
namespace SimpleTriggerCollider.Demo
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundDetection : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer; // Layer(s) to check for ground

        public bool IsGrounded { get; private set; }
        private Rigidbody2D rb;
        private new Collider2D collider;
        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (rb.linearVelocityY > 0.1f)
            {
                IsGrounded = false;
                return;
            }

            if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                IsGrounded = true;
            }
        }
    }
}