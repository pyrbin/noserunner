using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    public List<Interactable> interactableList = new List<Interactable>();

    public float minSize = 0f;

    PhysicsEvents physicsEvents;
    // Start is called before the first frame update
    void Start()
    {
        physicsEvents = GetComponent<PhysicsEvents>();
        physicsEvents.TriggerEnter += (collider) =>
        {
            Trigger(collider);
        };
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Trigger(Collider collider) {
        Slime slime = collider.GetComponentInParent<Slime>();
        if (slime && slime.Size >= minSize) {
            foreach(Interactable interactable in interactableList){
                interactable.Interact(null);
            }
        }
    }

}
