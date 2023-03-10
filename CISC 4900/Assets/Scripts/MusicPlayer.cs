using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource AudioSource;

    private float musicVolume = 1f;

    private void Start()
    {
        
    }

    private void Update()
    {
        AudioSource.volume = musicVolume;
    }

    public void UpdateVolumeSlider(float volume)
    {
        musicVolume = volume;
    }


}
