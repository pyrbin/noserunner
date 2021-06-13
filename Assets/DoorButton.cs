using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable

{
    public MovableObject door;

    public override string InteractionDescription()
    {
        return "Open door";
    }

    protected override void OnInteract(Interactor user)
    {
        door.Move(null);
    }

}
