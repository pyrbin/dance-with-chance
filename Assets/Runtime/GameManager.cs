using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<GameObject> Lines;

    public int Health = 3;

    public int CurrentHealth { get; private set; }

    public static int Score { get; set; } = 0;

    public event Action<Note> HitNote;

    public event Action<Note> MissedNote;

    public event Action<int> ScoreAdded;

    public event Action<int> ComboHappened;
    public event Action ComboLost;

    public event Action OnDeath;

    [HideInInspector]
    public static int ComboCounter = 0;

    public static GameManager instance { get; private set; }

    public static class Settings
    {
        public static (int Min, int Max) DiceRange = (1, 6);
    }

    public static int ComboRound(int i)
    {
        return Math.Max(5, (int)(Math.Ceiling(i / 5f) * 5));
    }

    void ToggleComboEffects(bool status)
    {
        foreach (var item in Lines)
        {
            item.transform.GetChild(0).gameObject.SetActive(status);
        }
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

                if (ComboCounter == ComboRound(ComboCounter))
                {
                    ToggleComboEffects(true);
                    CurrentHealth = math.min(CurrentHealth + 1, Health);
                    ComboHappened?.Invoke(ComboCounter);
                }
            }
            else
            {
                MissedNote?.Invoke(note);
                CurrentHealth--;

                ToggleComboEffects(false);

                if (ComboCounter > 0)
                {
                    ComboLost?.Invoke();
                }

                ComboCounter = 0;

                if (CurrentHealth == 0)
                {
                    TrackPlayer.Stop();
                    OnDeath?.Invoke();
                    SceneManager.LoadScene("GameOver");
                }
            }

            HitEffectSpawner.DoEffect(lane, !correct);

            NoteSpawner.Despawn(note);
        };
    }

    void Start()
    {
        ComboCounter = 0;
        Score = 0;
        TrackPlayer.Play();
    }
}
