using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType { BackgroundMusic, SFX, UI, Tutorial}
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
                AkSoundEngine.SetRTPCValue("VolumeMusic", newValue);
                break;
            case SoundType.SFX:
                AkSoundEngine.SetRTPCValue("VolumeSFX", newValue);
                break;
            case SoundType.UI:
                AkSoundEngine.SetRTPCValue("VolumeUI", newValue);
                break;
            case SoundType.Tutorial:
                AkSoundEngine.SetRTPCValue("VolumeTutorial", newValue);
                break;
        }
        
    }
}
