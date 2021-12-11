using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Note : MonoBehaviour
{

    public Sprite[] SideSprites = new Sprite[6];

    private SpriteRenderer Renderer;

    [NaughtyAttributes.ReadOnly]
    public int Number;

    [NaughtyAttributes.ReadOnly]
    public bool IsMoving = false;

    private void Awake()
    {
        TryGetComponent(out Renderer);

        Number = UnityEngine.Random.Range
        (
            GameManager.Settings.DiceRange.Min,
            GameManager.Settings.DiceRange.Max
        );

        Renderer.sprite = SideSprites[Number-1];
    }

    public void Move(Transform target, float travelSpeed, float distanceOffset = 0)
    {
        IsMoving = true;
        StartCoroutine(MoveTowards(target.position, travelSpeed, distanceOffset));
    }

    IEnumerator MoveTowards(float3 target, float duration, float distanceOffset = 0)
    {
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            var currentPos = transform.position;
            var time = (math.distance(currentPos, target) - distanceOffset) / (duration - counter) * Time.deltaTime;

            transform.position = Vector3.MoveTowards(currentPos, target, time);

            yield return null;
        }

        IsMoving = false;
    }
}
