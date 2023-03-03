using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
 
public AudioMixer aMixer;

public void SetVolume (float volume){
        aMixer.SetFloat("MasterVolume", volume);
        Debug.Log(volume);
    }

public void QualitySetting (int QIndex){

    QualitySettings.SetQualityLevel(QIndex);
}

public void FullScreenSetting (bool isFullScreen){
    Screen.fullScreen = isFullScreen;
}

public void OptVolume (float volume1){
        Debug.Log(volume1);
    }

}
