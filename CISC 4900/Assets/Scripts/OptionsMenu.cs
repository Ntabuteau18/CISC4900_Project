using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/**
 * This is the options menu that only allows a user to change settings to their desire.
 * The only settings we have to offer are a fullscreen setting and an volume slider that changes
 * the volume to your desired level.
 * 
 * */
public class OptionsMenu : MonoBehaviour
{
 
public AudioMixer aMixer;

public void SetVolume (float volume){
        aMixer.SetFloat("MasterVolume", volume);
        Debug.Log(volume);
    }


public void FullScreenSetting (bool isFullScreen){
    Screen.fullScreen = isFullScreen;
}

public void OptVolume (float volume1){
        Debug.Log(volume1);
    }

}
