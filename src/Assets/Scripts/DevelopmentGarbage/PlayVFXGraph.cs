using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayVFXGraph : MonoBehaviour
{
    [SerializeField] private VisualEffect correctPotionPoof;
    [SerializeField] private VisualEffect trashPotionPoof;

    public void PlayCorrectPotion()
    {
        correctPotionPoof.Play();
    }

    public void PlayTrashPotion()
    {
        trashPotionPoof.Play();
    }
}
