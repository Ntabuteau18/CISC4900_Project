using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Slider is made for the volume slider to see the value settings change as the player moves the slider
 * either to the right or the left.
 * */
public class VolumeSlider : MonoBehaviour
{
    public void SetVolume (float volume){
        Debug.Log(volume);
    }

}
