using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class FVXHandler : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [Tooltip("This is the id of which material will be the highlight. Could change on elements iwth more than one material.")]
    [SerializeField] private int idOfHighlight;
    [SerializeField] private Material highlightMaterial;

    public void ActivateHighLight()
    {
        print("Activating highlight in " + idOfHighlight);
        Material[] materials = meshRenderer.materials;
        Destroy(materials[idOfHighlight]);
        materials[idOfHighlight] = highlightMaterial; // We are saying which is the material, so I do not need to add and do weird stuff...this problem does not exist yet
        meshRenderer.materials = materials;
        meshRenderer.materials[idOfHighlight] = highlightMaterial; 
    }

    public void DeactivateHighlight()
    {
        int numberOfCurrentMaterials = meshRenderer.materials.Length;
        print("Deactivating highlight #" + (numberOfCurrentMaterials - 1) );
        Material[] materials = meshRenderer.materials;
        Destroy(materials[numberOfCurrentMaterials - 1]);
        materials[numberOfCurrentMaterials - 1] = null;
        meshRenderer.materials = materials;

    }
}
