using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slime : MonoBehaviour
{
    [SerializeField]
    public CharacterMovement Movement;

    public float InitialSize = 2f;

    [SerializeField]
    public SlimeSpecies Species;

    public float Size
    {
        get => transform.localScale.x / Species.Mods.Scale;
        set => transform.localScale =
            new float3(value * Species.Mods.Scale, value * Species.Mods.Scale, value * Species.Mods.Scale);
    }

    public float Speed => Species.BaseSpeed * Species.Mods.Speed * Size;

    public float JumpHeight => Species.BaseJumpHeight * Species.Mods.Jump * Size;

    private void Awake()
    {
        Movement ??= GetComponent<CharacterMovement>();
        Size = InitialSize;
    }

    public float2 MoveInput
    {
        set => Movement.MoveInput = value;
        get => Movement.MoveInput;
    }

    public void Jump()
    {
        // Movement.JumpHeight = JumpHeight;
        Movement.Jump();
    }

    public void Update()
    {

    }

    public void EnableControls()
    {
        Movement.Freeze();
    }

    public void DisableControls()
    {

    }
}
