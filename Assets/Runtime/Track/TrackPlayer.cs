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

    public void Play()
    {
        var notes = Track.Notes.ToList();
        notes.Sort();

        noteBuffer = new Queue<float>(notes);

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

    void Update()
    {
        if (Music.isPlaying)
            WhenPlaying();
    }

    void WhenPlaying()
    {
        if (noteBuffer is not { Count: > 0 }) return;
        
        var next = noteBuffer.ElementAt(0);

        if (next <= ElapsedInSeconds() + AheadTime)
        {
            IncomingNote.Invoke(next);
            noteBuffer.Dequeue();
        }
     
    }
}
