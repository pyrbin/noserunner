using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour

{
    public Vector3 target;
    public float speed;


    Vector3 startPos;
    Vector3 endPos;

    private bool isMoving = false;


    void Start()
    {
        startPos = transform.position;
        endPos = startPos + target;
    }


    void Update()
    {
        if (isMoving && Vector3.Distance(transform.position, endPos) >= 0.001f)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        }
        else if (!isMoving && Vector3.Distance(transform.position, startPos) >= 0.001f)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
        }

    }


    public void Move(Interactor user)
    {
        isMoving = true;

    }


    public void StopMove()
    {
        isMoving = false;

    }


}
