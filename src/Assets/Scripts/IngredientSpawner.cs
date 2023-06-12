using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    [SerializeField] private IngredientData ingredientData;
    // Start is called before the first frame update
    void Start()
    {
        SpawnIngredient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float VarianceToValue(float variance)
    {
        return Random.value * variance - variance / 2;
    }
    
    private void SpawnIngredient()
    {
        GameObject noob = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
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
    }
}
