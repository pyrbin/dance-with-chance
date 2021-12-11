using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(PhysicsEvents2D))]
public class Goal : MonoBehaviour
{
    public BoxCollider2D Collider { get; private set; }
    public PhysicsEvents2D PhysicsEvents { get; private set; }

    public event Action<Note> OnNoteEntered;

    void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        PhysicsEvents = GetComponent<PhysicsEvents2D>();

        PhysicsEvents.TriggerEnter += (go) =>
        {
            if (!go.TryGetComponent<Note>(out var note))
                return;

            OnNoteEntered?.Invoke(note);
        };
    }


}
