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

    public event Action<Interactable> Found;
    public event Action<Interactable> Lost;

    public void Interact()
    {
        if (currInteractable) currInteractable.Interact(this);
    }

    private bool isEnabled = true;

    public void EnableRaycasts()
    {
        isEnabled = false;
        if (currInteractable)
        {
            Lost?.Invoke(currInteractable);
            currInteractable = null;
        }
    }

    public void DisableRaycasts()
    {
        isEnabled = false;
        if (currInteractable)
        {
            Lost?.Invoke(currInteractable);
            currInteractable = null;
        }
    }

    void Update()
    {
        if (!isEnabled) return;
        if (!FindInteractable(layerMaskInteract, rayLength))
        {
            FindInteractable(layerMaskSlime, slimeRayLength);
        }
    }

    bool FindInteractable(int layerMask, int length)
    {
        Interactable interactable = null;

        if (CastRay(length, layerMask, out var hit))
        {
            interactable = hit.transform.GetComponent<Interactable>();
            if (interactable)
            {
                if (currInteractable != interactable)
                {
                    if (currInteractable) Lost?.Invoke(currInteractable);
                    currInteractable = interactable;
                    Found?.Invoke(currInteractable);
                }
            }
            else
            {
                if (currInteractable)
                {
                    Lost?.Invoke(currInteractable);
                    currInteractable = null;
                }
            }

            DebugDraw.Line(Camera.main.transform.position, hit.transform.position, interactable != null ? Color.green : Color.red);
        }
        else
        {
            if (currInteractable)
            {
                Lost?.Invoke(currInteractable);
                currInteractable = null;
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
