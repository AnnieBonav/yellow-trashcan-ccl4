using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayVFXGraph : MonoBehaviour
{
    
    //Annie did you know that start is triggered before the first frame is rendered?

    [SerializeField] private VisualEffect goodPoof;
    [SerializeField] private VisualEffect sadPoof;

    public void Good()
    {
        goodPoof.Play();
    }

    public void Sad()
    {
        sadPoof.Play();
    }
}
