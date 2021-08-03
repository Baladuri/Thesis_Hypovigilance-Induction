using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    // Create the audio name, audioclip source file and audio functionalities ie volume, pitch and spatialBlend
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialBlend;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
