using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectSpawner : MonoBehaviour
{
    public Animator Hit;

    public Animator Miss;


    void Awake()
    {
        Miss.gameObject.SetActive(true);
        Hit.gameObject.SetActive(true);
    }

    public void DoEffect(Goal.Lane lane, bool miss = false)
    {
        RotateByGoalLane(lane);
        var effectAnimator = miss ? Miss : Hit;
        effectAnimator.SetTrigger("Play");
    }

    void RotateByGoalLane(Goal.Lane lane)
    {
        var rotation = lane switch
        {
            Goal.Lane.Top => 0,
            Goal.Lane.Bottom => 180,
            Goal.Lane.Left => 90,
            Goal.Lane.Right => -90,
            _ => 0
        };

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
