using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static Dice;

public class DiceController : MonoBehaviour
{
    public Dice dice;
    public PlayerHoldDrag holdDrag;

    public float cooldown = 0.3f;
    private float currentCooldown = 0f;
    private bool locked = false;

    void Awake() {
        holdDrag.Released += drag => DragReleased(drag);
    }

    void Update() {
        if (locked) {
            currentCooldown += Time.deltaTime;
            if (currentCooldown > cooldown) {
                locked = false;
                currentCooldown = 0f;
            }
        }
    }

    public void DragReleased(float2 drag) {
        if (locked) return;
        float angle = math.atan2(drag.x, drag.y)*180/math.PI;
        if (angle >= -45 && angle <= 45) Flip(FlipDirection.Down);
        if (angle >= -135 && angle <= -45) Flip(FlipDirection.Right);
        if (angle >= 45 && angle <= 135) Flip(FlipDirection.Left);
        if (angle <= -135 || angle >= 135) Flip(FlipDirection.Up);
    }

    public void OnKeyPressed(InputAction.CallbackContext context) {
        if (locked) return;
        if (context.action.triggered) {
            var direction = context.action.ReadValue<Vector2>();
            if (direction == Vector2.down) Flip(FlipDirection.Down); 
            if (direction == Vector2.up) Flip(FlipDirection.Up); 
            if (direction == Vector2.right) Flip(FlipDirection.Right); 
            if (direction == Vector2.left) Flip(FlipDirection.Left); 
        }
    }

    public void Flip(FlipDirection direction) {
        if (direction == FlipDirection.Down) dice.FlipDown(); 
        if (direction == FlipDirection.Up) dice.FlipUp(); 
        if (direction == FlipDirection.Right) dice.FlipRight(); 
        if (direction == FlipDirection.Left) dice.FlipLeft(); 
        locked = true;
        Debug.Log(dice.Top);
    }

    
}
