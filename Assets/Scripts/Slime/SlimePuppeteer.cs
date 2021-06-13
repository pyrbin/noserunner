using System;
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
    public ThrowIndicator Indicator;

    private void Awake()
    {
        if (CurrentSlime)
            SwitchSlime(CurrentSlime);
    }

    public float3 LookDirection()
    {
        return math.normalizesafe((float3)Camera.main.transform.TransformDirection(Vector3.forward).normalized + new float3(0f, 0.3f, 0));
    }

    public bool IsShooting => holdSplit || stopShoot;

    bool holdSplit = false;

    public float force = 0f;

    [Header("Throw forces")]
    public float2 minMaxForce = new float2(3f, 10f);
    public float forceGainPerSeconds = 2f;
    public float horizontalForceDamp = 0.55f;
    public float shootCooldownInSeconds = 1f;
    public event Action<Slime> SlimeChanged;



    private bool stopShoot = false;

    public void Split(InputAction.CallbackContext context)
    {
        if (CurrentSlime == null || CurrentSlime.Size == 1f || stopShoot) return;

        var shoot = holdSplit && !context.action.triggered;

        holdSplit = context.action.triggered;

        var look = LookDirection();
        Indicator.Clear();

        if (shoot && CurrentSlime.Split(out var slime, LocalFireLocation()))
        {
            stopShoot = true;

            CurrentSlime.GetComponentInChildren<Collider>().enabled = false;
            slime.Movement.ApplyForce(ForceWithDamp(look));
            slime.DisableControls();
            Timer.Register(shootCooldownInSeconds, () =>
            {
                try
                {
                    stopShoot = false;
                }
                catch
                {

                }
            });

            force = minMaxForce.x;
            SlimeChanged?.Invoke(CurrentSlime);

            Timer.Register(.1f, () =>
            {
                try
                {
                    CurrentSlime.GetComponentInChildren<Collider>().enabled = true;
                }
                catch
                {

                }
            });
        }
    }


    public float3 ForceWithDamp(float3 look)
    {
        var forceWithDir = look * force;
        forceWithDir.x *= horizontalForceDamp;
        forceWithDir.z *= horizontalForceDamp;
        return forceWithDir;
    }

    public float3 LocalFireLocation()
    {
        var pos = ((float3)LookDirection()) * (CurrentSlime.Radius / 2f + 0.65f);
        return pos;
    }

    private void Update()
    {
        if (CurrentSlime)
            Indicator.GroundMask = CurrentSlime.Movement.GroundMask;

        if (holdSplit)
        {
            force += Time.deltaTime * forceGainPerSeconds;
            force = math.clamp(force, minMaxForce.x, minMaxForce.y);

            Indicator.Simulate(
                (float3)CurrentSlime.transform.position + LocalFireLocation(),
                ForceWithDamp(LookDirection()),
                CurrentSlime.Movement.Gravity,
                Time.deltaTime
            );
        }
    }

    public void SwitchSlime(Slime slime)
    {
        if (IsShooting) return;
        if (slime.AboutToBeConsumed) return;

        var last = CurrentSlime;

        if (slime != CurrentSlime)
        {
            CurrentSlime?.DisableControls();
            CurrentSlime = slime;
            slime.EnableControls();

        }

        if (last && last != CurrentSlime)
        {
            //
            CurrentSlime.transform.LookAt(last.transform);
        }

        VirtualCamera.Follow = slime.transform;
        SlimeChanged?.Invoke(slime);
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
