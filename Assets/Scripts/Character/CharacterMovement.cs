using System;
using Unity.Mathematics;
using UnityConstantsGenerator;
using UnityEngine;

#pragma warning disable 649




public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    CharacterController Controller;

    [HideInInspector]
    public float Speed = 10f;
    [Range(0, .3f)]
    [SerializeField]
    private float MovementSmoothing = .05f;

    public float2 MoveInput { get; set; }
    private float3 verticalVelocity = float3.zero;

    [SerializeField]
    public LayerMask GroundMask;

    [SerializeField]
    public float JumpHeight = 1.0f;

    [SerializeField]
    public float Gravity = -9.81f;

    [SerializeField]
    float CheckGroundedOffset = 0.1f;

    [Header("Debug Draw")]
    bool DrawDebug = true;

    bool IsGrounded { get; set; } = true;
    bool jump = false;

    Transform cameraTransform;

    public event Action<bool> OnGrounded;

    bool Freezed = false;

    Vector3 Velocity = Vector3.zero;

    public void Jump()
    {
        jump = true;
    }

    public void Freeze()
    {
        MoveInput = float2.zero;
        Freezed = true;
    }

    public void Unfreeze()
    {
        Freezed = false;
    }

    public void Awake()
    {
        Controller ??= GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void FixedUpdate()
    {
        if (!Freezed) ApplyMovement();
        ApplyGravity();
    }

    public void ApplyForce(float3 force)
    {
        verticalVelocity = force;
    }

    private void ApplyMovement()
    {
        var input = new float3(MoveInput.x, 0f, MoveInput.y);
        var move = cameraTransform.forward * input.z + cameraTransform.right * input.x;
        move.y = 0f;

        var moveForce = move * Speed;
        var smoothed = Vector3.SmoothDamp(Controller.velocity, moveForce, ref Velocity, MovementSmoothing);
        smoothed.y = 0f;

        Controller.Move(smoothed * Time.fixedDeltaTime);

        if (DrawDebug)
        {
            DebugDraw.Line(transform.position, transform.position + moveForce, Color.cyan);
        }

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    private void ApplyGravity()
    {
        var groundCheckOffset = (float3)transform.position + new float3(0f, -(Controller.height * transform.localScale.y / 2f) - CheckGroundedOffset / 2f, 0f);
        var isGrounded = Physics.CheckSphere(groundCheckOffset, CheckGroundedOffset, GroundMask);

        if (isGrounded != IsGrounded)
        {
            OnGrounded?.Invoke(isGrounded);
        }

        IsGrounded = isGrounded;
        if (DrawDebug)
        {
            DebugDraw.Sphere(groundCheckOffset, CheckGroundedOffset, IsGrounded ? Color.green : Color.red);
        }

        if (IsGrounded)
        {
            verticalVelocity.y = 0f;
            if (Freezed || (math.abs(verticalVelocity.x) > 0f || math.abs(verticalVelocity.z) > 0f))
            {
                verticalVelocity.x = 0f;
                verticalVelocity.z = 0f;
            }
        }

        // Changes the height position of the player..
        if (jump && IsGrounded)
        {
            verticalVelocity.y += math.sqrt(JumpHeight * -3.0f * Gravity);
        }

        verticalVelocity.y += Gravity * Time.fixedDeltaTime;

        Controller.Move(verticalVelocity * Time.fixedDeltaTime);

        jump = false;
    }
}
