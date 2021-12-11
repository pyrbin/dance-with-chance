using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(PhysicsEvents2D))]
public class Goal : MonoBehaviour
{
    public enum Lane
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    public BoxCollider2D Collider { get; private set; }
    public PhysicsEvents2D PhysicsEvents { get; private set; }

    public event Action<Note, Lane> OnNoteEntered;

    void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        PhysicsEvents = GetComponent<PhysicsEvents2D>();

        PhysicsEvents.TriggerEnter += (go) =>
        {
            if (!go.TryGetComponent<Note>(out var note))
                return;

            OnNoteEntered?.Invoke(note, DetermineLane(note));
        };
    }


    Lane DetermineLane(Note note)
    {
        if (note.transform.position.x > transform.position.x)
        {
            return Lane.Right;
        }

        if (note.transform.position.x < transform.position.x)
        {
            return Lane.Left;
        }

        if (note.transform.position.y < transform.position.y)
        {
            return Lane.Bottom;
        }

        if (note.transform.position.y > transform.position.y)
        {
            return Lane.Top;
        }

        return Lane.Top;
    }

}
