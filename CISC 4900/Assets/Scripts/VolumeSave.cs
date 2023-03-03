using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
