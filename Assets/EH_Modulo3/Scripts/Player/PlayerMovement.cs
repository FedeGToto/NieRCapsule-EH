using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpPower = 8;
    private float turnSmoothVelocity;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("Camera")]
    [SerializeField] private Transform originalCamera;
    [SerializeField] private Vector3 shoulderOffset;
    [SerializeField] private Transform cameraAnchorPoint;
    [SerializeField] private float xSensitivity = 5f;
    [SerializeField] private float ySensitivity = 5f;

    private CharacterController cc;
    private Vector3 velocity;
    private PlayerTargetLock targetLock;

    public Vector3 DirectionToMove { get; private set; }
    public CharacterController GetCharacterController() => cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        targetLock = GetComponent<PlayerTargetLock>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateMovement()
    {
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Jump();

        ApplyGravity();

        Move();

        UpdateCamera();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(InputManager.HORIZONTALMOVE, 0, InputManager.VERTICALMOVE);

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + originalCamera.eulerAngles.y;
            float newAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (!targetLock.IsTargetLocked)
                transform.rotation = Quaternion.Euler(0f, newAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            DirectionToMove = moveDirection.normalized;
            cc.Move(moveSpeed * Time.deltaTime * moveDirection.normalized);
        }
    }

    private void UpdateCamera()
    {
        cameraAnchorPoint.position = transform.position + shoulderOffset;

        Vector2 normMouse = new Vector2(InputManager.HORIZONTALLOOK, InputManager.VERTICALLOOK).normalized;

        cameraAnchorPoint.rotation *= Quaternion.AngleAxis(normMouse.x * xSensitivity * Time.deltaTime, Vector3.up);
        cameraAnchorPoint.rotation *= Quaternion.AngleAxis(normMouse.y * ySensitivity * Time.deltaTime, Vector3.right);

        var angles = cameraAnchorPoint.localEulerAngles;
        angles.z = 0;

        var angle = cameraAnchorPoint.localEulerAngles.x;

        // Clamp the camera
        if (angle > 180 && angle < 290)
        {
            angles.x = 290;
        }
        else if (angle < 180 && angle > 70)
        {
            angles.x = 70;
        }

        cameraAnchorPoint.localEulerAngles = angles;
    }

    private void Jump()
    {
        if (IsGrounded() && InputManager.JUMP)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }


}
