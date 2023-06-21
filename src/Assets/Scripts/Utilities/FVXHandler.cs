using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class FVXHandler : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [Tooltip("This is the id of which material will be the highlight. Could change on elements iwth more than one material.")]
    [SerializeField] private Material highlightMaterial;
    private int idOfHighlight = 0;

    public void ActivateHighLight()
    {
        print("Activating highlight");
        meshRenderer.AddMaterial(highlightMaterial);
        Material[] materials = meshRenderer.materials;
        idOfHighlight = meshRenderer.materials.Length - 1;

        print("New id: " +  idOfHighlight);
    }

    public void DeactivateHighlight()
    {
        int numberOfCurrentMaterials = meshRenderer.materials.Length;
        print("Deactivating highlight #" + (numberOfCurrentMaterials - 1) );
        Material[] materials = new Material[idOfHighlight]; // Creates an array with the amount of materials we need without conuting the highlight
        for(int i = 0; i < materials.Length - 1; i++)
        {
            materials[i] = meshRenderer.materials[i];
        }
        
        meshRenderer.materials = materials;

    }
}
