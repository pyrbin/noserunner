using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleInteractable : Interactable
{
    protected override void OnInteract(Interactor user)
    {
        Debug.Log("You have interacted with me");
    }
}
