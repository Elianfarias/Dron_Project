using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class DroneMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerSettingsSO data;
    [SerializeField] private Transform droneVisual;


    private Rigidbody rb;
    private HealthSystem healthSystem;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystem>();
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

    private void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    private void OnLook(InputValue value) => lookInput = value.Get<Vector2>();
    private void OnUpDown(InputValue value) => verticalInput = value.Get<float>();

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
        ClampVelocity();
        ApplyVisualTilt();
    }

    private void OnCollisionEnter(Collision other)
    {
        healthSystem.DoDamage(other.relativeVelocity.magnitude * 2);
    }

    private void ApplyMovement()
    {
        Vector3 localDirection = new(moveInput.x, verticalInput, moveInput.y);
        Vector3 worldDirection = transform.TransformDirection(localDirection);
        rb.AddForce(worldDirection * data.Force);
        rb.AddForce(data.VerticalForce * verticalInput * Vector3.up);
    }

    private void ApplyRotation()
    {
        float angle = lookInput.x * data.RotationSpeedX * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, angle, Space.World);
    }

    private void ClampVelocity()
    {
        Vector3 horizontal = new(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float vertical = rb.linearVelocity.y;

        horizontal = Vector3.ClampMagnitude(horizontal, data.MaxHorizontalSpeed);
        vertical = Mathf.Clamp(vertical, -data.MaxVerticalSpeed, data.MaxVerticalSpeed);

        rb.linearVelocity = new Vector3(horizontal.x, vertical, horizontal.z);
    }

    private void ApplyVisualTilt()
    {
        float targetPitch = moveInput.y <= 0 ? (moveInput.y * data.TiltAngle) : (moveInput.y * data.TiltAngle * 0.5f);
        float targetRoll = -moveInput.x * data.TiltAngle;

        Quaternion playerRotation = Quaternion.Euler(targetPitch, 0f, targetRoll);

        droneVisual.localRotation = Quaternion.Lerp(
            droneVisual.localRotation,
            playerRotation,
            Time.fixedDeltaTime * data.TiltSpeed
            );
    }
}