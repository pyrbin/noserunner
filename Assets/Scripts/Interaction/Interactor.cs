using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityTimer;

public class Interactor : MonoBehaviour
{
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private int slimeRayLength = 100;
    [SerializeField] private LayerMask layerMaskSlime;

    public Interactable currInteractable;
    public Interactable currInteractableSwitchSlime;

    private SlimePuppeteer puppeteer;

    public event Action<Interactable> Found;
    public event Action<Interactable> Lost;

    public void Awake()
    {
        puppeteer ??= GetComponent<SlimePuppeteer>();
    }

    bool disableSwitchTo = false;
    bool disableInteract = false;

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.action.triggered || disableInteract) return;
        if (currInteractable) currInteractable.Interact(this);
        disableInteract = true;
        Timer.Register(0.5f, () =>
        {
            try { disableInteract = false; }
            catch { }
        });
    }

    public void SwitchTo(InputAction.CallbackContext context)
    {
        if (!context.action.triggered || disableInteract) return;
        if (currInteractable != null && currInteractable != currInteractableSwitchSlime) return;
        if (currInteractableSwitchSlime == null) return;

        if (currInteractableSwitchSlime.TryGetComponent<Slime>(out var slime))
        {
            puppeteer.SwitchSlime(slime);
            disableSwitchTo = true;
            Timer.Register(0.5f, () =>
            {
                try { disableSwitchTo = false; }
                catch { }
            });
        }
    }

    private bool isEnabled = true;

    void Update()
    {
        if (!isEnabled) return;

        var current = currInteractable;
        FindInteractable(layerMaskInteract, rayLength, ref currInteractable);
        FindInteractable(layerMaskSlime, slimeRayLength, ref currInteractableSwitchSlime);

        if (currInteractable != null && currInteractable != current)
            Found?.Invoke(currInteractable);
    }

    bool FindInteractable(int layerMask, int length, ref Interactable current)
    {
        Interactable interactable = null;

        if (CastRay(length, layerMask, out var hit))
        {
            interactable = hit.transform.GetComponent<Interactable>();
            if (interactable)
            {
                if (current != interactable)
                {
                    if (current) Lost?.Invoke(current);
                    current = interactable;
                }
            }
            else
            {
                if (current)
                {
                    Lost?.Invoke(current);
                    current = null;
                }
            }

            DebugDraw.Line(Camera.main.transform.position, hit.transform.position, interactable != null ? Color.green : Color.red);
        }
        else
        {
            if (current)
            {
                Lost?.Invoke(current);
                current = null;
            }
        }

        return interactable != null;
    }

    bool CastRay(int length, int layerMask, out RaycastHit hit)
    {
        var fwd = Camera.main.transform.TransformDirection(Vector3.forward);
        var result = Physics.Raycast(Camera.main.transform.position, fwd, out hit, length, layerMask);
        return result;
    }
}
