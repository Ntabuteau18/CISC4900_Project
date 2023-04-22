using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Volume Saver is the actual way we save the selected value the slider lands on, so once the player
 * picks a setting this saves the setting and loads the volume value selected.
 */
public class VolumeSave : MonoBehaviour
{
   [SerializeField] private Slider volumeSlider = null;
   [SerializeField] private Text volumeTextUI = null;

    public void VolumeSlider(float volume){
    volumeTextUI.text = volume.ToString("0.0");
   }

    public void SaveVolumeButton(){
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
        Debug.Log("Saved Volume Value");
    }

    public void LoadValues(){
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }



}
