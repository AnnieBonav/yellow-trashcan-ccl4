using System;
using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private List<RecipeData> recipes;
    private IngredientDictionary _currentIngredients;

    private RecipeData CurrentPotion()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].Ingredients.Equals(_currentIngredients)) return recipes[i];
        }

        return null;
    }

    private void AddIngredient(IngredientType type)
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

        RecipeData currentPotion = CurrentPotion();
        if(currentPotion is not null) Debug.Log($"Currently {currentPotion.name}");
        else
        {
            Debug.Log("Currently not a known potion");
        }
    }
}
