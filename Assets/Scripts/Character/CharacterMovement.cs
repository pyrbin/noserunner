using Unity.Mathematics;
using UnityEngine;

#pragma warning disable 649




public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    CharacterController Controller;

    [SerializeField]
    float Speed = 11f;

    public float2 MoveInput { get; set; }
    private float3 verticalVelocity = float3.zero;

    [SerializeField]
    float JumpHeight = 1.0f;

    [SerializeField]
    float Gravity = -9.81f;

    bool IsGrounded => Controller.isGrounded;
    bool jump = false;

    Transform cameraTransform;

    public void Jump()
    {
        jump = true;
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
        ApplyMovement();
        ApplyJump();
    }

    public void ApplyMovement()
    {
        // clamp vertical velocity if grounded
        if (IsGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = 0f;
        }

        var input = new float3(MoveInput.x, 0f, MoveInput.y);
        var move = cameraTransform.forward * input.z + cameraTransform.right * input.x;
        move.y = 0f;

        Controller.Move(move * Time.deltaTime * Speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void ApplyJump()
    {
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
