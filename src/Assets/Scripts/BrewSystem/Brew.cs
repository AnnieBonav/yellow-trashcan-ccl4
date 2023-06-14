using System;
using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;

public class Brew : MonoBehaviour
{
    [SerializeField] private List<RecipeData> recipes;
    [SerializeField] private IngredientDictionary _currentIngredients;
    [SerializeField] private Transform _potionSpawnOrigin;
    [SerializeField] private bool _debugIngredientsIn;

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
            case IngredientType.Liquid:
                _currentIngredients.liquid++;
                break;
            case IngredientType.Mushroom:
                _currentIngredients.mushroom++;
                break;
            case IngredientType.Herb:
                _currentIngredients.herb++;
                break;
            case IngredientType.Bark:
                _currentIngredients.bark++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        
        if (_debugIngredientsIn)
        {
            RecipeData currentBrew = CurrentBrew();

            if (currentBrew is not null) Debug.Log($"Currently {currentBrew.name}");
            else
            {
                Debug.Log("Currently not a known potion");
            }
        }
    }

    public void MakePotion(GameObject flask)
    {
        RecipeData currentBrew = CurrentBrew();

        if (currentBrew is not null)
        {
            Debug.Log($"You made a {currentBrew.name}!!");
            GameObject noobPotion = Instantiate(currentBrew.PotionPrefab);
            noobPotion.transform.position = _potionSpawnOrigin.transform.position;
        }
        else
        {
            Debug.Log("You made trash.");
        }
        Destroy(flask.transform.parent.gameObject);
    }
}
