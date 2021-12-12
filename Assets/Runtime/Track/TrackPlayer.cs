using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class TrackPlayer : MonoBehaviour
{
    public Track Track;
    public event Action<float> IncomingNote;

    private AudioSource Music;
    private float AheadTime;
    private Queue<float> noteBuffer;

    private bool isPlaying = false;
    private bool pitchChangeGuard = false;

    void Awake()
    {
        Music = GetComponent<AudioSource>();
        Music.playOnAwake = false;
        Music.pitch = 1;
        Music.Stop();

        if (Track != null)
            SetTrack(Track);
    }

    public float? ElapsedInSeconds()
    {
        return Music.isPlaying ? Music.time : null;
    }

    public void SetAheadTime(float value)
    {
        AheadTime = value;
    }

    public void SetTrack(Track track)
    {
        Track = track;
    }

    public void Stop()
    {
        Music.Stop();
        isPlaying = false;
    }

    public void Play(float pitch = 1f)
    {
        Music.pitch = pitch;
        isPlaying = true;
        InitTrackNotes();

        if (Music.isPlaying)
        {
            Music.Stop();
            Music.clip = Track.Audio;
            Music.Play();
        }
        else
        {
            Music.clip = Track.Audio;
            Music.Play();
        }
    }

    void InitTrackNotes()
    {
        var notes = Track.Notes.ToList();
        notes.Sort();
        noteBuffer = new Queue<float>(notes);
    }

    void Update()
    {
        if (Music.isPlaying && isPlaying)
        {
            WhenPlaying();
        }
    }

    void WhenPlaying()
    {
        const float approxOffset = 0.099999f;

        if (Music.clip.length - Music.time <= approxOffset && !pitchChangeGuard)
        {
            Music.pitch += 0.15f;
            pitchChangeGuard = true;
            InitTrackNotes();
            return;
        }

        if (Music.time < approxOffset && Music.time >= 0)
        {
            pitchChangeGuard = false;
        }


        if (noteBuffer is not { Count: > 0 })
        {
            return;
        }
        
        var next = noteBuffer.ElementAt(0);

        if (next <= ElapsedInSeconds() + AheadTime)
        {
            IncomingNote.Invoke(next);
            noteBuffer.Dequeue();
        }
     
    }
}
