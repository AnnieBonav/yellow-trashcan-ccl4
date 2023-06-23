using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Ingredient prefabToSpawn;
    [SerializeField] private IngredientData ingredientData;
    
    private float VarianceToValue(float variance)
    {
        return Random.value * variance - variance / 2;
    }
    
    public Ingredient SpawnIngredient()
    {
        GameObject noob = Instantiate(prefabToSpawn.gameObject, transform.position, Quaternion.identity);
        Ingredient ingredient = noob.GetComponent<Ingredient>();
        ingredient.IngredientType = ingredientData.IngredientType;
        
        ingredient.ColourModifier = new Color(
            ingredientData.ColourModifier.r + VarianceToValue(ingredientData.ColourVariance),
            ingredientData.ColourModifier.g + VarianceToValue(ingredientData.ColourVariance),
            ingredientData.ColourModifier.b + VarianceToValue(ingredientData.ColourVariance)
        );

        ingredient.IntensityModifier =
            ingredientData.IntensityModifier + VarianceToValue(ingredientData.IntensityVariance);
        ingredient.SwirlModifier = ingredientData.SwirlModifier + VarianceToValue(ingredientData.SwirlVariance);

        return ingredient;
    }
}
