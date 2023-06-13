using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BrewProperties),typeof(Brew))]
public class IngredientAcceptor : MonoBehaviour
{
    private BrewProperties _brewProperties;
    private Brew _brew;

    private void Awake()
    {
        _brew = GetComponent<Brew>();
        _brewProperties = GetComponent<BrewProperties>();
    }

    public void Take(Ingredient ingredient)
    {
        if(ingredient is null) return;
        _brew.AddIngredient(ingredient.IngredientType);
        _brewProperties.AddColour(ingredient.ColourModifier);
        _brewProperties.AddBubbling(ingredient.IntensityModifier);
        _brewProperties.AddSwirl(ingredient.SwirlModifier);
        Destroy(ingredient.gameObject);
    }
}
