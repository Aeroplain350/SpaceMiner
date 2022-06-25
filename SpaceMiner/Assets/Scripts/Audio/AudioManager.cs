using UnityEngine;
using UnityEngine.Audio;
using System;

// manages the sound clips
// thanks brackeys
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Scripts")]
    public MixerManager mixerManager;

    [Header("Sound Clips")]
    public Sound[] sounds;

    private void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        // looks through the sound clips for the same name
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // returns if the sound is not found
        if (s == null) return;

        // gets the sound clips enum sound type
        string soundType = s.soundType.ToString();

        // finds the mixer from the resources folder in assets
        AudioMixer mixer = Resources.Load("AudioMixer") as AudioMixer;

        // finds the specific mixer and sets
        AudioMixerGroup[] audioMixerGroup = mixer.FindMatchingGroups(soundType);
        s.source.outputAudioMixerGroup = audioMixerGroup[0];

        s.source.pitch = s.pitch;
        s.source.Play();
    }
}
