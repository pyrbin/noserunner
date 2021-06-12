using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    public List<Interactable> interactableList = new List<Interactable>();

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
            Interactor user = collider.gameObject.GetComponent<Interactor>();
            Debug.Log("Triggered!!");
            Debug.Log(user);
            Debug.Log(collider.name);
            if (user) {
                foreach(Interactable interactable in interactableList){
                    interactable.Interact(user);
                }
            }
    }

}
