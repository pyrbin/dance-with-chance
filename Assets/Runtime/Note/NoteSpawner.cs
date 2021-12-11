using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    public Goal Target;

    [SerializeField]
    public List<GameObject> SpawnPoints;

    [SerializeField]
    public Note NotePrefab;

    public float TravelSpeed = 3f;

    [SerializeField]
    public TrackPlayer TrackPlayer;

    [NaughtyAttributes.ReadOnly]
    public List<float> Distances;

    void Awake()
    {
        Distances?.Clear();
        Distances ??= new List<float>();

        foreach (var sp in SpawnPoints)
        {
            Distances.Add(math.distance(sp.transform.position, Target.transform.position));
        }

        var same = !Distances.Distinct().Skip(1).Any();

        if (!same)
        {
            Debug.LogError($"Distances are not the same: {string.Join(",", Distances)}");
        }

    }

    void Start()
    {
        TrackPlayer.SetAheadTime(TravelSpeed);
        TrackPlayer.IncomingNote += OnIncommingNote;
    }

    void OnIncommingNote(float timestamp)
    {
        Spawn().Move(Target.transform, TravelSpeed, Target.Collider.size.x / 2f);
    }

    public Note Spawn()
    {
        var index = UnityEngine.Random.Range(0, SpawnPoints.Count - 1);

        var spawnPoint = (SpawnPoints[index]).transform.position;
        var note = (Instantiate(NotePrefab, spawnPoint, quaternion.identity, transform)).GetComponent<Note>();
        return note;
    }

    public void Despawn(Note note, bool? success = null)
    {
        note.gameObject.SetActive(false);
        Destroy(note.gameObject);
    }
}
