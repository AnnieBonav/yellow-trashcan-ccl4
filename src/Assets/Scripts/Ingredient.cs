using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private Color colourModifier;
    [SerializeField] private float intensityModifier;
    [SerializeField] private float swirlModifier;
        
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
    
}
