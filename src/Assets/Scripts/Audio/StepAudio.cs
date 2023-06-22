using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event audioEvent;
    
    public void Step()
    {
        audioEvent.Post(gameObject);
    }
}
