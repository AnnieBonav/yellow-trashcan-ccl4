using UnityEngine;


public enum IngredientType
{
    Placeholder1,
    Placeholder2
}

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient")] 
public class IngredientData : ScriptableObject
{
    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private float intensityModifier;
    [SerializeField] private float intensityVariance;
    [SerializeField] private float swirlModifier;
    [SerializeField] private float swirlVariance;
    [SerializeField] private Color colourModifier;
    [SerializeField] private float colourVariance;

    public IngredientType IngredientType
    {
        get { return ingredientType; }
        private set { ingredientType = value; }
    }

    public float IntensityModifier
    {
        get { return intensityModifier; }
        private set { intensityModifier = value;  }
    }
    
    public float IntensityVariance
    {
        get { return intensityVariance; }
        private set { intensityVariance = value; }
    }

    public float SwirlModifier
    {
        get { return swirlModifier; }
        private set { swirlModifier = value; }
    }

    public float SwirlVariance
    {
        get { return swirlVariance; }
        private set { swirlVariance = value; }
    }

    public Color ColourModifier
    {
        get { return colourModifier; }
        private set { colourModifier = value; }
    }

    public float ColourVariance
    {
        get { return colourVariance; }
        private set { colourVariance = value; }
    }
    
}

