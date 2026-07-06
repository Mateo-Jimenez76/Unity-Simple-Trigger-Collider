using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleTriggerCollider.Demo
{
    //Required For Applying forces
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Run Settings")]
        [Tooltip("The max speed that the player can reach while running(Not a velocity cap!!)")]
        [SerializeField] private float _maxSpeed = 10;
        [SerializeField] private float _acceleration = 7;
        [SerializeField] private float _deceleration = 7;
        [SerializeField] private float _velPower = 0.9f;
        [Tooltip("Friction is applied to help slow the player down once they have let go of the movement buttons")]
        [SerializeField] private float _frictionAmount = 0.2f;

        [Header("Jump Settings")]
        [Tooltip("The force to apply in the upwards direction when the jump key is pressed")]
        [SerializeField] private float _jumpForce = 15;
        [Tooltip("A buffer allowing for the player to still jump even after a set amount of time off the ground")]
        [SerializeField] private float _jumpCoyoteTime = 0.15f;

        private GroundDetection _groundDetection;
        private float _horizontalInput;
        private float _coyoteTimeCounter;
        private Rigidbody2D _rb;
        private InputAction _move;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rb.freezeRotation = true;
            _move = InputSystem.actions["Move"];
            InputSystem.actions["Jump"].performed += context => TryJump();

            _groundDetection = GetComponentInChildren<GroundDetection>();
        }

        // Update is called once per frame
        void Update()
        {
            _horizontalInput = _move.ReadValue<Vector2>().x;

            if (_groundDetection.IsGrounded)
            {
                _coyoteTimeCounter = _jumpCoyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
        }
        private void FixedUpdate()
        {
            #region Friction
            if (Mathf.Abs(_horizontalInput) <= 0.01)
            {
                float amount = Mathf.Min(Mathf.Abs(_rb.linearVelocityX), Mathf.Abs(_frictionAmount));
                amount *= Mathf.Sign(_rb.linearVelocityX);
                _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }
            #endregion

            #region Run
            float targetSpeed = _horizontalInput * _maxSpeed;
            float speedDif = targetSpeed - _rb.linearVelocityX;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);
            _rb.AddForce(movement * Vector2.right);
            #endregion
        }

        private void Jump()
        {
            _coyoteTimeCounter = 0;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void TryJump()
        {
            if (_coyoteTimeCounter > 0)
            {
                Jump();
            }
        }

        public void ApplyBoost(float force)
        {
            _rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        }
    }
}