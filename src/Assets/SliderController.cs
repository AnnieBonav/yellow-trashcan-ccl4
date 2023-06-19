using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType { BackgroundMusic, SFX, UI}
public class SliderController : MonoBehaviour
{
    [SerializeField] private SoundType soundType;
    [SerializeField] private Slider slider;
    
    public void HandleSliderChange()
    {
        float newValue = slider.value;
        switch(soundType)
        {
            case SoundType.BackgroundMusic:
                print("New music volume: " + newValue);
                AkSoundEngine.SetRTPCValue("MusicVolume", newValue);
                break;
            case SoundType.SFX:
                AkSoundEngine.SetRTPCValue("SFXVolume", newValue);
                break;
            case SoundType.UI:
                AkSoundEngine.SetRTPCValue("UIVolume", newValue);
                break;
        }
        
    }
}
