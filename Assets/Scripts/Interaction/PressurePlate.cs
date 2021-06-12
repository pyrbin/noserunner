using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    public List<MovableObject> movablesList = new List<MovableObject>();

    public float minSize = 0f;

    PhysicsEvents physicsEvents;
    // Start is called before the first frame update
    void Start()
    {
        physicsEvents = GetComponent<PhysicsEvents>();
        physicsEvents.TriggerEnter += (collider) =>
        {
            Slime slime = collider.GetComponentInParent<Slime>();
            if (slime && slime.Size >= minSize)
            {
                foreach (MovableObject movableObject in movablesList)
                {
                    movableObject.Move(null);
                }
            }
        };

        physicsEvents.TriggerExit += (collider) =>
        {
            Slime slime = collider.GetComponentInParent<Slime>();
            if (slime && slime.Size >= minSize)
            {
                foreach (MovableObject movableObject in movablesList)
                {
                    movableObject.StopMove();
                }
            }
        };
    }



}
