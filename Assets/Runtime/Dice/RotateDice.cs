using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static Dice;
using static DiceController;

public class RotateDice : MonoBehaviour
{

    public Dice dice;
    public float duration = 0.3f;


    void Awake() {
        dice.Flipped += (direction) => {

            Vector3 rotation = new Vector3(0, 0, 0);

            if (direction == FlipDirection.Left) rotation = new Vector3(0, 90, 0);
            if (direction == FlipDirection.Right) rotation = new Vector3(0, -90, 0);
            if (direction == FlipDirection.Down) rotation = new Vector3(-90, 0, 0);
            if (direction == FlipDirection.Up) rotation = new Vector3(90, 0, 0);
            
            StartCoroutine(RotateOverSeconds(rotation, duration));
        };
    }

    public IEnumerator RotateOverSeconds (Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.rotation.eulerAngles;
        while (elapsedTime < seconds)
        {
            transform.RotateAround(transform.position, Vector3.up, end.y / seconds * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.right, end.x / seconds * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    
        transform.rotation = Quaternion.Euler(startingPos.x + end.x, startingPos.y + end.y, 0);

    }

    
}
