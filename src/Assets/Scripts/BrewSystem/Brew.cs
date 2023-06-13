using System;
using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;

public class Brew : MonoBehaviour
{
    [SerializeField] private List<RecipeData> recipes;
    private IngredientDictionary _currentIngredients;

    private RecipeData CurrentBrew()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].Ingredients.Equals(_currentIngredients)) return recipes[i];
        }

        return null;
    }

    public void AddIngredient(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Placeholder1:
                _currentIngredients.placeholder1++;
                break;
            case IngredientType.Placeholder2:
                _currentIngredients.placeholder2++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        RecipeData currentBrew = CurrentBrew();
        if(currentBrew is not null) Debug.Log($"Currently {currentBrew.name}");
        else
        {
            Debug.Log("Currently not a known potion");
        }
    }
}
