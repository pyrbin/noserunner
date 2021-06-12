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
    public float Speed = 11f;

    public float2 MoveInput { get; set; }
    private float3 verticalVelocity = float3.zero;

    [SerializeField]
    LayerMask GroundMask;

    [SerializeField]
    public float JumpHeight = 1.0f;

    [SerializeField]
    float Gravity = -9.81f;

    [Header("Debug Draw")]
    bool DrawDebug = true;

    bool IsGrounded { get; set; } = true;
    bool jump = false;

    Transform cameraTransform;

    public event Action<bool> OnGrounded;

    bool Freezed = false;

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

    public void Update()
    {
        if (!Freezed) ApplyMovement();
        ApplyGravity();
    }

    public void Throw(float3 dir, float force, float hdamp = 1f)
    {
        verticalVelocity = dir * force;
        verticalVelocity.x *= hdamp;
        verticalVelocity.z *= hdamp;

    }

    private void ApplyMovement()
    {
        var input = new float3(MoveInput.x, 0f, MoveInput.y);
        var move = cameraTransform.forward * input.z + cameraTransform.right * input.x;
        move.y = 0f;

        var moveForce = move * Time.deltaTime * Speed;
        Controller.Move(moveForce);

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
        const float checkRadius = 0.1f;
        var groundCheckOffset = (float3)transform.position + new float3(0f, -(Controller.height * transform.localScale.y / 2f), 0f);

        var isGrounded = Physics.CheckSphere(groundCheckOffset, checkRadius, GroundMask);

        if (isGrounded != IsGrounded)
        {
            OnGrounded?.Invoke(isGrounded);
        }

        IsGrounded = isGrounded;

        if (DrawDebug)
        {
            DebugDraw.Sphere(groundCheckOffset, checkRadius, IsGrounded ? Color.green : Color.red);
        }

        if (IsGrounded)
        {
            verticalVelocity.y = 0f;

            if (Freezed || (verticalVelocity.x > 0f || verticalVelocity.z > 0f))
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

        verticalVelocity.y += Gravity * Time.deltaTime;
        Controller.Move(verticalVelocity * Time.deltaTime);

        jump = false;
    }
}
