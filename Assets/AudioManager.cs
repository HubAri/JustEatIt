using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
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
        // DontDestroyOnLoad(gameObject);

        foreach (AudioSound s in sounds)
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
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void Pause(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }
    public void UnPause(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.UnPause();
    }
    public void LowerPitch(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = 0.75f;
    }
    public void NormalPitch(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = 1f;
    }
}