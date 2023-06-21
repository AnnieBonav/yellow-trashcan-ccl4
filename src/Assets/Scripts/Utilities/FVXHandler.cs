using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class FVXHandler : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshRenderers;
    [Tooltip("This is the id of which material will be the highlight. Could change on elements iwth more than one material.")]
    [SerializeField] private Material highlightMaterial;
    private int idOfHighlight = 0;

    public void ActivateHighLight()
    {
        print("Activating highlight");
        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.AddMaterial(highlightMaterial);
            idOfHighlight = meshRenderer.materials.Length - 1;

            print("New id: " + idOfHighlight);
        }
        
    }

    public void DeactivateHighlight()
    {
        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            int numberOfCurrentMaterials = meshRenderer.materials.Length;
            print("Deactivating highlight #" + (numberOfCurrentMaterials - 1));
            Material[] materials = new Material[idOfHighlight]; // Creates an array with the amount of materials we need without conuting the highlight
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = meshRenderer.materials[i];
            }

            meshRenderer.materials = materials;
        }
    }
}
