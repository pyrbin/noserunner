using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Events;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider2D))]
public class PhysicsEvents2D : MonoBehaviour
{
    public event Action<Collision2D> CollisionEnter;
    public event Action<Collision2D> CollisionExit;

    public event Action<Collider2D> TriggerEnter;
    public event Action<Collider2D> TriggerExit;

    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) => TriggerEnter?.Invoke(other);
    private void OnTriggerExit2D(Collider2D other) => TriggerExit?.Invoke(other);

    private void OnCollisionEnter2D(Collision2D other) => CollisionEnter?.Invoke(other);
    private void OnCollisionExit2D(Collision2D other) => CollisionExit?.Invoke(other);

}
