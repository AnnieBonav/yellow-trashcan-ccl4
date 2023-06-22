using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BrewProperties),typeof(Brew))]
public class IngredientAcceptor : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
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

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collided");
        if (collider.gameObject.CompareTag("Ingredient"))
        {
            AkSoundEngine.PostEvent("Play_PutIngredient", gameObject);
            Take(collider.gameObject.GetComponentInParent<Ingredient>());
            interactionsHandler.RaiseInteraction(InteractionEvents.PutIngredientPot);
            return;
        }

        if (collider.gameObject.CompareTag("Flask"))
        {
            print("Collided with flask");
            _brew.MakePotion(collider.gameObject);
            _brewProperties.ResetColor();
        }
    }
}
