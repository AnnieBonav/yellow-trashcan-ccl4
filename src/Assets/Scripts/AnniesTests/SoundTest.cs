using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Material _hoverMaterial;
    public void PlayPop()
    {
        // SOunds every time you hover it, even when you are grabbing with one hand and hovering with the other
        AkSoundEngine.PostEvent("Play_Pop", gameObject);
    }

    public void ChangeToBaseMaterial(bool isBase)
    {
        // Changes when you grab the object
        if(isBase)
        {
            _renderer.material = _baseMaterial;
        }
        else
        {
            _renderer.material = _hoverMaterial;
        }
    }
}
