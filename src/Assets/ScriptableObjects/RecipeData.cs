using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private IngredientDictionary ingredients;
    [SerializeField] private GameObject potionPrefab;
    
    public IngredientDictionary Ingredients => ingredients;
    public GameObject PotionPrefab => potionPrefab;
}
