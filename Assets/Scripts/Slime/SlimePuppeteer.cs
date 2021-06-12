using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimePuppeteer : MonoBehaviour
{
    public int Current = 0;

    [SerializeField]
    public List<Slime> Slimes = new List<Slime>();

    public Slime CurrentSlime => Slimes?.Count > 0 ? Slimes?[Current] : null;

    public CinemachineVirtualCamera VirtualCamera;

    private void Awake()
    {
        if (CurrentSlime)
            SwitchSlime(Current);
    }

    public void AddSlimeToList(Slime slime)
    {
        Slimes.Add(slime);
    }

    public void SwitchToNext()
    {
        var nextIndex = (Current + 1) % (Slimes.Count);
        SwitchSlime(nextIndex);
    }

    public void SwitchSlime(int index)
    {
        if (index >= Slimes.Count) return;

        var slime = Slimes[index];

        if (slime != CurrentSlime)
        {
            CurrentSlime?.DisableControls();
            slime.EnableControls();
        }

        var last = CurrentSlime;

        Current = index;

        if (last)
        {
            CurrentSlime.transform.LookAt(last.transform);
        }

        VirtualCamera.Follow = slime.transform;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        CurrentSlime.MoveInput = (float2)context.ReadValue<Vector2>();
    }

    public void OnJump()
    {
        CurrentSlime.Jump();
    }
}
