using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    public List<MovableObject> movablesList = new List<MovableObject>();

    public float minSize = 0f;

    PhysicsEvents physicsEvents;


    public Slime currentSlime;

    // Start is called before the first frame update
    void Start()
    {
        physicsEvents = GetComponent<PhysicsEvents>();
        physicsEvents.TriggerEnter += (collider) =>
        {

            if (collider.transform.parent.transform.parent.TryGetComponent<Slime>(out var slime)
                && slime.Size >= minSize)
            {
                currentSlime = slime;
                foreach (MovableObject movableObject in movablesList)
                {
                    movableObject.Move(null);
                }
            }
        };

        physicsEvents.TriggerExit += (collider) =>
        {
            try
            {
                if (collider.transform.parent.transform.parent.TryGetComponent<Slime>(out var slime) && slime == currentSlime)
                {
                    currentSlime = null;
                    StopMove();
                }
            }
            catch
            {
                //ignored
            }

        };
    }

    void StopMove()
    {
        foreach (MovableObject movableObject in movablesList)
        {
            movableObject.StopMove();
        }
    }

    void Update()
    {
        if (currentSlime && currentSlime.Size < minSize)
        {
            StopMove();
            currentSlime = null;
        }
    }



}
