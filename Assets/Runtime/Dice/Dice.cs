using System;
using UnityEngine;


public class Dice : MonoBehaviour
{
    public enum FlipDirection
    {
        Left,
        Right,
        Up, 
        Down
    }

    public event Action<FlipDirection> Flipped;

    // Top, Right, Up, Left, Down, Bottom
    private int[] Configuration;

    public int Top => Configuration[0];

    public int Right => Configuration[1];

    public int Up => Configuration[2];

    public int Left => Configuration[3];

    public int Down => Configuration[4];

    public int Bottom => Configuration[5];


    // Start is called before the first frame update
    void Start()
    {
        Configuration = new int[]{1, 2, 3, 5, 4, 6};
    }

    public void FlipRight() {
        int[] newConfiguration = new int[]{
            Left,
            Top,
            Up,
            Bottom,
            Down,
            Right
        };
        Configuration = newConfiguration;
        Flipped?.Invoke(FlipDirection.Right);
    }

    
    public void FlipLeft() {
        int[] newConfiguration = new int[]{
            Right,
            Bottom,
            Up,
            Top,
            Down,
            Left
        };
        Configuration = newConfiguration;
        Flipped?.Invoke(FlipDirection.Left);
    }

    
    public void FlipDown() {
        int[] newConfiguration = new int[]{
            Up,
            Right,
            Bottom,
            Left,
            Top,
            Down
        };
        Configuration = newConfiguration;
        Flipped?.Invoke(FlipDirection.Down);
    }

    
    public void FlipUp() {
        int[] newConfiguration = new int[]{
            Down,
            Right,
            Top,
            Left,
            Bottom,
            Up
        };
        Configuration = newConfiguration;
        Flipped?.Invoke(FlipDirection.Up);
    }
    

    
}
