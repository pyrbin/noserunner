using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
public class PhysicsEvents : MonoBehaviour
{
    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;

    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;

    public Collider Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) => TriggerEnter?.Invoke(other);
    private void OnTriggerExit(Collider other) => TriggerExit?.Invoke(other);

    private void OnCollisionEnter(Collision other) => CollisionEnter?.Invoke(other);
    private void OnCollisionExit(Collision other) => CollisionExit?.Invoke(other);

}
