using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable()]
public struct SoundParameters
{
    [Range(0,1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;
    public bool Loop;
}

[System.Serializable()]
public class Sound
{
    [SerializeField] string name;
    public string Name { get { return name; } }

    [SerializeField] AudioClip clip;
    public AudioClip Clip { get { return clip; } }

    [SerializeField] SoundParameters parameters;
    public SoundParameters Parameters { get { return parameters; } }

    [HideInInspector]
    public AudioSource Source;

    public void Play()
    {
        Source.clip = Clip;

        Source.volume = Parameters.Volume;
        Source.pitch = Parameters.Pitch;
        Source.loop = Parameters.Loop;

        Source.Play();

    }

    public void Stop()
    {
        Source.Stop();

    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] sounds;
    [SerializeField] AudioSource sourcePrefab;

    [SerializeField] string StarterMusic;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitSounds();

        if(string.IsNullOrEmpty(StarterMusic) != true)
        {
            PlaySound(StarterMusic);
        }
    }

    void InitSounds()
    {
        foreach (var sound in sounds)
        {
            AudioSource source = (AudioSource)Instantiate(sourcePrefab, gameObject.transform);
            source.name = sound.Name;
            sound.Source = source;
        }
    }

    public void PlaySound(string name)
    {
        var sound = GetSound(name);
        if(sound != null)
        {
            sound.Play();
        }
        else
        {
            Debug.LogWarningFormat("Sound not found");
        }
    }
    public void StopSound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.Stop();
        }
        else
        {
            Debug.LogWarningFormat("Sound not found.");
        }
    }
    Sound GetSound(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.Name == name)
            {
                return sound;
            }
        }
        return null;
    }

    public void StopAndLoadScene()
    {
        // stop all audio
        //AudioManager.Instance.StopAllSounds();
      
        // load the new scene
        SceneManager.LoadScene(0);
    }

    public void StopAllSounds()
    {
        foreach (var sound in sounds)
        {
            sound.Stop();
        }
    }
}

