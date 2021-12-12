using System;
using Unity.Mathematics;
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
    public HitEffectSpawner HitEffectSpawner;

    [NaughtyAttributes.Required]
    public Goal Goal;

    public int Health = 3;

    public int CurrentHealth { get; private set; }

    public int Score { get; set; } = 0;

    public event Action<Note> HitNote;

    public event Action<Note> MissedNote;

    public event Action<int> ScoreAdded;

    public event Action OnDeath;

    [HideInInspector]
    public int ComboCounter = 0;

    public static GameManager instance { get; private set; }

    public static class Settings
    {
        public static (int Min, int Max) DiceRange = (1, 6);
        public static int ComboTarget = 5;
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

        Goal.OnNoteEntered += (note, lane) =>
        {
            var correct = Dice.Top == note.Number;

            if (correct)
            {
                Score++;
                ComboCounter++;
                ScoreAdded?.Invoke(Score);
                HitNote?.Invoke(note);

                if (ComboCounter == Settings.ComboTarget)
                {
                    CurrentHealth = math.min(CurrentHealth + 1, Health);
                }
            }
            else
            {
                MissedNote?.Invoke(note);
                CurrentHealth--;
                ComboCounter = 0;

                if (CurrentHealth == 0)
                {
                    OnDeath?.Invoke();
                }
            }

            HitEffectSpawner.DoEffect(lane, !correct);

            NoteSpawner.Despawn(note);
        };
    }

    void Start()
    {
        TrackPlayer.Play();
    }
}
