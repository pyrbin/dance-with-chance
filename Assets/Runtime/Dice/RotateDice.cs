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

    public GameObject rotatable;

    public Vector3 dir = Vector3.right;

    Vector3 currentEulerAngles;
    Quaternion currentRotation;

    void Awake() {

        currentEulerAngles = Vector3.zero;

        dice.Flipped += (direction) => {

            Vector3 rotation = new Vector3(0,0,0);


            if (direction == FlipDirection.Right) rotation = new Vector3(0, -90, 0);
            if (direction == FlipDirection.Left) rotation = new Vector3(0, 90, 0);
            if (direction == FlipDirection.Down) rotation = new Vector3(-90, 0, 0);
            if (direction == FlipDirection.Up) rotation = new Vector3(90, 0, 0);
            

            StartCoroutine(RotateOverSeconds(rotation, duration));
        };
    }

    public IEnumerator RotateOverSeconds (Vector3 end, float seconds)
    {
        float elapsedTime = 0;
 
        float2 total = new float2(0,0);
        while (elapsedTime < seconds)
        {

            total.x += end.x / seconds * Time.deltaTime;
            rotatable.transform.RotateAround(transform.position, transform.up, end.y / seconds * Time.deltaTime);
            total.y += end.y / seconds * Time.deltaTime;
            rotatable.transform.RotateAround(transform.position, transform.right, end.x / seconds * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rotatable.transform.RotateAround(transform.position, transform.up, -total.y);
        rotatable.transform.RotateAround(transform.position, transform.right, -total.x);


        rotatable.transform.RotateAround(transform.position, transform.up, end.y);
        rotatable.transform.RotateAround(transform.position, transform.right, end.x );



    }

    private float clampEuler(float angle) {
        if (angle >= 180) {
            return angle -= 360;
        }
        if (angle <= -180) {
            return angle += 360;
        }
        return angle;
    }

    
}
