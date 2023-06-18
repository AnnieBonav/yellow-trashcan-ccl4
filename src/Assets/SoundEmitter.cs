using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private string soundEventName;

    public void PlaySound()
    {
        AkSoundEngine.PostEvent(soundEventName, gameObject);
        print("Playing pick ingredient");
    }
}
