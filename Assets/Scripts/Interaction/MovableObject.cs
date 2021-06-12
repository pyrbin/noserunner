using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : Interactable
{
    public Vector3 target;
    public float timeToReachTarget;

    float t;
    Vector3 startPosition;
    private bool isMoving = false;

    protected override void OnInteract(Interactor user)
    {
        isMoving = true;
    }


    void Start()
    {
        startPosition = transform.position;
        target = startPosition + target;
    }


    void Update()
    {
        if (isMoving)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition, target, t);
        }
    }

}
