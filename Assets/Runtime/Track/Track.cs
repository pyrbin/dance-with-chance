using UnityEngine;

[CreateAssetMenu(fileName = "Track", menuName = "ScriptableObjects/Track", order = 1)]
public class Track : ScriptableObject
{
    [SerializeField]
    public AudioClip Audio;

    [SerializeField]
    public float[] Notes;
}
