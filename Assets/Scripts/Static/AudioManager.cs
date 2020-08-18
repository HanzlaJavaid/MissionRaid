using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    public static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.shoot,
            Resources.Load<AudioClip>("shoot"));
        audioClips.Add(AudioClipName.die,
            Resources.Load<AudioClip>("die1"));
        audioClips.Add(AudioClipName.die2,
            Resources.Load<AudioClip>("die2"));
        audioClips.Add(AudioClipName.die3,
            Resources.Load<AudioClip>("die3"));
        audioClips.Add(AudioClipName.playerdie,
            Resources.Load<AudioClip>("playerdie"));
        audioClips.Add(AudioClipName.blast,
            Resources.Load<AudioClip>("b"));
        audioClips.Add(AudioClipName.pass1,
            Resources.Load<AudioClip>("pass1"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        if(name == AudioClipName.die)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    name = name;
                    break;
                case 1:
                    name = AudioClipName.die2;
                    break;
                case 2:
                    name = AudioClipName.die3;
                    break;
            }
        }
        audioSource.PlayOneShot(audioClips[name]);
    }
}
