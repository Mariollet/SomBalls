using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;
    public Sound[] sounds;
    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = audioMixerGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("FunkMenu");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }
    public void Unpause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.UnPause();
    }
    public void PlayLooping(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        s.source.Play();

    }
    public void SlowDownPitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = 0.8f;
    }
    public void SlowlyDownPitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = 0.9f;
    }
    public void FastUpPitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = 1f;
    }
    public void DownVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume -= 0.11f;
    }
    public void UpVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume += 0.11f;
    }
    public void MuteVolume()
    {
        audioMixer.SetFloat("Volume", -80f);
    }
    public void UnmuteVolume()
    {
        audioMixer.SetFloat("Volume", 0f);
    }
}
