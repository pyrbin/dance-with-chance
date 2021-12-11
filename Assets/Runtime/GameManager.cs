using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public string Seed = "SEED";

    [NaughtyAttributes.Required]
    public TrackPlayer TrackPlayer;

    [NaughtyAttributes.Required]
    public Dice Dice;

    [NaughtyAttributes.Required]
    public NoteSpawner NoteSpawner;

    [NaughtyAttributes.Required]
    public Goal Goal;

    public int Health = 3;

    public int CurrentHealth { get; private set; }

    public int Score { get; set; } = 0;

    public event Action<Note> HitNote;

    public event Action<Note> MissedNote;

    public event Action<int> ScoreAdded;

    public event Action OnDeath;

    [NaughtyAttributes.Button("Test Play")]
    public void TestPlay()
    {
        TrackPlayer.Play();
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

        CurrentHealth = Health;

        UnityEngine.Random.InitState(Seed.GetHashCode());

        Goal.OnNoteEntered += (note) =>
        {
            var correct = Dice.Top == note.Number;

            if (correct)
            {
                Score++;
                ScoreAdded?.Invoke(Score);
                HitNote?.Invoke(note);
            }
            else
            {
                MissedNote?.Invoke(note);
                CurrentHealth--;

                if (CurrentHealth == 0)
                {
                    OnDeath?.Invoke();
                }
            }

            NoteSpawner.Despawn(note);
        };
    }
}
