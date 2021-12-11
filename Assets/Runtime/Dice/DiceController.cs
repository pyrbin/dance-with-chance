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

    public float cooldown = 0.3f;
    private float currentCooldown = 0f;
    private bool locked = false;

    void Update() {
        if (locked) {
            currentCooldown += Time.deltaTime;
            if (currentCooldown > cooldown) {
                locked = false;
                currentCooldown = 0f;
            }
        }
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
    }

    
}
