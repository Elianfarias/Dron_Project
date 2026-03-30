using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class DroneMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float force = 10f;
    [SerializeField] private float verticalForce = 8f;

    [Header("Rotación")]
    [SerializeField] private float rotationSpeedX = 80f;
    [SerializeField] private float rotationSpeedY = 60f;
    [SerializeField] private float maxPitchAngle = 60f;

    [Header("Camera")]
    [SerializeField] private Camera playerCamera;


    // Referencias
    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void OnUpDown(InputValue value)
    {
        verticalInput = value.Get<float>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    private void ApplyMovement()
    {
        Vector3 localDirection = new(moveInput.x, verticalInput, moveInput.y);
        Vector3 worldDirection = transform.TransformDirection(localDirection);

        rb.AddForce(worldDirection * force);
        rb.AddForce(verticalForce * verticalInput * Vector3.up);
    }

    private void ApplyRotation()
    {
        float angle = lookInput.x * rotationSpeedX * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, angle, Space.World);
    }
}