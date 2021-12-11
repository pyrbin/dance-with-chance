using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public string Seed = "SEED";

    [NaughtyAttributes.Required]
    public TrackPlayer Player;

    [NaughtyAttributes.Required]
    public NoteSpawner NoteSpawner;

    [NaughtyAttributes.Required]
    public Goal Goal;

    public int Score {  get; set; }

    [NaughtyAttributes.Button("Test Play")]
    public void TestPlay()
    {
        Player.Play();
    }

    public static GameManager instance { get; private set; }

    public static class Settings
    {
        public static (int Min, int Max) DiceRange = (1, 6);
    }

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        UnityEngine.Random.InitState(Seed.GetHashCode());

        Goal.OnNoteEntered += (note) =>
        {
            // TODO(pyrbin) check if Dice has correct number for incoming dice.

            NoteSpawner.Despawn(note);
        };
    }
}
