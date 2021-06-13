using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ThrowIndicator : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public int MaxSimulations = 100;
    public LayerMask GroundMask;


    void Start()
    {

    }

    public void Update()
    {
        float width = LineRenderer.startWidth;
        LineRenderer.material.mainTextureScale = new Vector2(1f / width, 1.0f);
    }

    public void Simulate(float3 origin, float3 force, float gravity, float deltaTime)
    {
        Clear();
        LineRenderer.positionCount = MaxSimulations;

        for (int i = 0; i < MaxSimulations; i++)
        {
            LineRenderer.SetPosition(i, origin);
            force.y += gravity * deltaTime;
            origin += (force * deltaTime);
            if (Physics.CheckSphere(origin, .1f, GroundMask))
            {
                LineRenderer.SetPosition(i + 1, origin);
                LineRenderer.positionCount = i + 1;
                break;
            }
        }
    }

    public void Clear()
    {
        LineRenderer.positionCount = 0;
    }
}
