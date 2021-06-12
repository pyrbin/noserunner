using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleInteractable : Interactable
{
    public override string InteractionDescription()
    {
        return "it hurts pleas stop";
    }

    protected override void OnInteract(Interactor user)
    {
        Debug.Log("You have interacted with me");
    }
}
