using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

    [SerializeField] private Camera origin;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    private Interactable currInteractable;

    public event Action<Interactable> Found;
    public event Action<Interactable> Lost;

    public void Interact()
    {
        if (currInteractable) currInteractable.Interact(this);
    }


    void Update()
    {
        FindInteractable();
    }

    void FindInteractable()
    {
        RaycastHit hit;
        Vector3 fwd = origin.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(origin.transform.position, fwd * 10, Color.red);
        Interactable interactable = null;

        if (Physics.Raycast(origin.transform.position, fwd, out hit, rayLength, layerMaskInteract))
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
        }
        else
        {
            if (currInteractable)
            {
                Lost?.Invoke(currInteractable);
                currInteractable = null;
            }
        }
    }


}
