using Unity.Mathematics;
using UnityConstantsGenerator;
using UnityEngine;
using UnityTimer;

public class Slime : Interactable
{
    [SerializeField]
    public CharacterMovement Movement;

    public GameObject SlimePrefab;

    public override string InteractionDescription()
    {
        return "Merge slime";
    }

    public float InitialSize = 1f;

    [SerializeField]
    public SlimeSpecies Species;

    public Timer ScaleTimer;

    public float Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
            var targetScale = new float3(size * Species.Mods.Scale, size * Species.Mods.Scale, size * Species.Mods.Scale);

            if (ScaleTimer != null)
            {
                ScaleTimer.Cancel();
            }

            float transitionDuration = 1.5f;
            ScaleTimer = Timer.Register(transitionDuration,
               onUpdate: elapsed =>
                {
                    try
                    {
                        transform.localScale = math.lerp(transform.localScale, targetScale, elapsed / transitionDuration);
                    }
                    catch
                    {
                        // ignore
                    }
                },
               onComplete: () => { });
        }
    }

    private float size = 1f;

    public float Speed => Species.BaseSpeed * Species.Mods.Speed / Size;

    public float Radius => transform.localScale.x;

    public float JumpHeight => Species.BaseJumpHeight * Species.Mods.Jump / Size;

    public bool AboutToBeConsumed { get; private set; } = false;

    protected override void OnInteract(Interactor user)
    {
        if (AboutToBeConsumed) return;

        var puppeteer = user.GetComponent<SlimePuppeteer>();

        if (puppeteer.CurrentSlime == this) return;

        puppeteer.CurrentSlime.Consume(this);
        AboutToBeConsumed = true;
        gameObject.SetActive(false);
    }

    public void Consume(Slime slime)
    {
        if (slime == this) return;

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
        Movement.Speed = Speed;
    }

    public void OnDisable()
    {
        ScaleTimer.Cancel();
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
