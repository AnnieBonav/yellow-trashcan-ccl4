using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private Color colourModifier;
    [SerializeField] private float intensityModifier;
    [SerializeField] private float swirlModifier;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public IngredientType IngredientType
    {
        get { return ingredientType; }
        set { ingredientType = value; }
    }

    public Color ColourModifier
    {
        get { return colourModifier; }
        set { colourModifier = value; }
    }

    public float IntensityModifier
    {
        get { return intensityModifier; }
        set { intensityModifier = value; }
    }

    public float SwirlModifier
    {
        get { return swirlModifier; }
        set { swirlModifier = value; }
    }

    private void OnDestroy()
    {
        EmptyGrabbable emptyContainer = GetComponentInParent<EmptyGrabbable>();
        if(emptyContainer != null)
        {
            print("It had an empty container");
            emptyContainer.DestroyContainer();
        }
    }

    public void GrabbedIngredient()
    {
        print("Grabbed ingredient");
        InteractionRaised?.Invoke(InteractionEvents.GrabIngredient);
    }

    public void ReleasedIngredient()
    {
        print("Released ingredient");
        // Does not get called
    }

    public void ResetFreeze()
    {
        print("Reset rb");
        rb.constraints = RigidbodyConstraints.None;
        InteractionRaised?.Invoke(InteractionEvents.ReleaseIngredient);
    }

}
