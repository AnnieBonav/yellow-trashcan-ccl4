using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private IngredientDictionary ingredients;
    
    public IngredientDictionary Ingredients => ingredients;
}
