using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterMovement CharacterMovement;

    public void Awake()
    {
        CharacterMovement ??= GetComponent<CharacterMovement>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        CharacterMovement.MoveInput = (float2)context.ReadValue<Vector2>();
    }

    public void OnJump()
    {
        CharacterMovement.Jump();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        // CharacterMovement.MouseInput = (float2)context.ReadValue<Vector2>();
    }
}
