using Unity.Mathematics;
using UnityConstantsGenerator;
using UnityEngine;

public class Slime : Interactable
{
    [SerializeField]
    public CharacterMovement Movement;

    public GameObject SlimePrefab;

    public float InitialSize = 1f;

    [SerializeField]
    public SlimeSpecies Species;

    public float Size
    {
        get => transform.localScale.x / Species.Mods.Scale;
        set => transform.localScale =
            new float3(value * Species.Mods.Scale, value * Species.Mods.Scale, value * Species.Mods.Scale);
    }

    public float Speed => Species.BaseSpeed * Species.Mods.Speed * Size;

    public float Radius => transform.localScale.x;

    public float JumpHeight => Species.BaseJumpHeight * Species.Mods.Jump * Size;

    private bool AboutToBeConsumed = false;

    protected override void OnInteract(Interactor user)
    {
        if (AboutToBeConsumed) return;
        var puppeteer = user.GetComponent<SlimePuppeteer>();
        puppeteer.CurrentSlime.Consume(this);
        AboutToBeConsumed = true;
        gameObject.SetActive(false);
    }

    public void Consume(Slime slime)
    {
        Size += slime.Size;
        Destroy(slime.gameObject);
    }

    public bool Split(out Slime slime, float3 offset)
    {
        slime = null;
        if (Size <= 1f) return false;

        var origin = (float3)transform.position + offset;
        // origin.y = Size * Species.Mods.Scale / 2f;

        var spawn = Instantiate(SlimePrefab, origin, quaternion.identity);

        Size -= 1f;


        slime = spawn.GetComponent<Slime>();
        slime.Size = 1f;

        return true;
    }

    private void Awake()
    {
        Movement ??= GetComponent<CharacterMovement>();
        Size = InitialSize;
        Movement.Unfreeze();
    }

    public float2 MoveInput
    {
        set => Movement.MoveInput = value;
        get => Movement.MoveInput;
    }

    public void Jump()
    {
        Movement.JumpHeight = JumpHeight;
        Movement.Jump();
    }

    void Update()
    {

    }

    public void EnableControls()
    {
        Movement.Unfreeze();
    }

    public void DisableControls()
    {
        Movement.Freeze();
    }
}
