using UnityEngine;
using UnityEngine.InputSystem;

//Required For Applying forces
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private float _horizontalInput;
    private float _verticalInput;
    private Rigidbody2D _rb;
    private InputAction _move;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rb.freezeRotation = true;

        _move = InputSystem.actions["Move"];

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = _move.ReadValue<Vector2>().x;
        _verticalInput = _move.ReadValue<Vector2>().y;
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_horizontalInput, _verticalInput) * _moveSpeed;
    }
}