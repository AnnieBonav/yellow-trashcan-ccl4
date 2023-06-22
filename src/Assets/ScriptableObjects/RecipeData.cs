using System.Collections;
using System.Collections.Generic;
using Dictionaries;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private IngredientDictionary ingredients;
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private Color potionColor;
    [SerializeField] private Sprite potionImage;
    
    public IngredientDictionary Ingredients => ingredients;
    public GameObject PotionPrefab => potionPrefab;
    public Color PotionColor => potionColor;
    public Sprite PotionImage => potionImage;
}
