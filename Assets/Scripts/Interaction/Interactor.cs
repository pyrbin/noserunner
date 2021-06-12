using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private int slimeRayLength = 100;
    [SerializeField] private LayerMask layerMaskSlime;

    private Interactable currInteractable;
    private Interactable currInteractableSwitchSlime;

    private SlimePuppeteer puppeteer;

    public event Action<Interactable> Found;
    public event Action<Interactable> Lost;

    public void Awake()
    {
        puppeteer ??= GetComponent<SlimePuppeteer>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;
        if (currInteractable) currInteractable.Interact(this);
    }

    public void SwitchTo(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;
        if (currInteractable != null && currInteractable != currInteractableSwitchSlime) return;
        if (currInteractableSwitchSlime == null) return;

        if (currInteractableSwitchSlime.TryGetComponent<Slime>(out var slime))
        {
            puppeteer.SwitchSlime(slime);
        }
    }

    private bool isEnabled = true;

    void Update()
    {
        if (!isEnabled) return;

        FindInteractable(layerMaskInteract, rayLength, ref currInteractable);
        FindInteractable(layerMaskSlime, slimeRayLength, ref currInteractableSwitchSlime);
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
                    Found?.Invoke(current);
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
