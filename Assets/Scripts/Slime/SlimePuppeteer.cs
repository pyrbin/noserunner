using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityConstantsGenerator;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityTimer;

public class SlimePuppeteer : MonoBehaviour
{
    public Slime CurrentSlime;

    public CinemachineVirtualCamera VirtualCamera;

    private void Awake()
    {
        if (CurrentSlime)
            SwitchSlime(CurrentSlime);
    }

    public float3 LookDirection()
    {
        return Camera.main.transform.TransformDirection(Vector3.forward).normalized;
    }

    bool holdSplit = false;

    float force = 0f;

    [Header("Throw forces")]
    public float2 minMaxForce = new float2(3f, 10f);
    public float forceGainPerSeconds = 2f;
    public float horizontalForceDamp = 0.75f;

    public void Split(InputAction.CallbackContext context)
    {
        if (CurrentSlime == null) return;

        var shoot = holdSplit && !context.action.triggered;

        holdSplit = context.action.triggered;

        if (shoot && CurrentSlime.Split(out var slime, LookDirection() * CurrentSlime.Radius))
        {
            slime.DisableControls();
            CurrentSlime.GetComponentInChildren<Collider>().enabled = false;

            slime.Movement.Throw(LookDirection(), force);
            Timer.Register(.1f, () =>
            {
                CurrentSlime.GetComponentInChildren<Collider>().enabled = true;
            });
            force = minMaxForce.x;
        }
    }

    private void Update()
    {
        if (holdSplit)
        {
            force += Time.deltaTime * forceGainPerSeconds;
            force = math.clamp(force, minMaxForce.x, minMaxForce.y);
        }
    }

    public void SwitchSlime(Slime slime)
    {
        if (slime != CurrentSlime)
        {
            CurrentSlime?.DisableControls();
            slime.EnableControls();
        }

        var last = CurrentSlime;
        CurrentSlime = slime;

        if (last && last != CurrentSlime)
        {
            CurrentSlime.transform.LookAt(last.transform);
        }

        VirtualCamera.Follow = slime.transform;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (CurrentSlime)
            CurrentSlime.MoveInput = (float2)context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;
        CurrentSlime.Jump();
    }
}
