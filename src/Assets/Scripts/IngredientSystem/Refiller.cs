using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Refiller : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private IngredientContainer ingredientContainer;

    [Header("refill if the container has children (herb and mushropom")]
    [SerializeField] private bool hasChildren;
    [SerializeField] private GameObject childrenIngredients;

    private void Start()
    {
        AkSoundEngine.SetSwitch("Ingredient", ingredientType.ToString(), gameObject);
    }
    public void RefillContainer()
    {
        bool couldRefill;
        if(ingredientType == IngredientType.Bark)
        {
            couldRefill = ingredientContainer.Refill();
        }
        else
        {
            couldRefill = ingredientContainer.RefillSingle(); // If could refill is false, then another sounds should be played
        }
        InteractedRefiller();
        if (couldRefill)
        {
            AkSoundEngine.PostEvent("Play_Refill", gameObject);
            if (!hasChildren) return;
            foreach(Transform child in childrenIngredients.GetComponentInChildren<Transform>())
            {
                if (child.gameObject.activeInHierarchy)
                {
                    child.gameObject.SetActive(false);
                    return;
                }
            }
        }
        else
        {
            AkSoundEngine.PostEvent("Play_NoMoreRefill", gameObject);
        }
    }

    private void InteractedRefiller()
    {
        switch (ingredientType)
        {
            case IngredientType.Bark:
                interactionsHandler.RaiseInteraction(InteractionEvents.RefilledBark);
                break;
            case IngredientType.Mushroom:
                interactionsHandler.RaiseInteraction(InteractionEvents.RefilledMushroom);
                break;
            case IngredientType.Liquid:
                interactionsHandler.RaiseInteraction(InteractionEvents.RefilledLiquid);
                break;
            case IngredientType.Herb:
                interactionsHandler.RaiseInteraction(InteractionEvents.RefilledHerb);
                break;
        }
    }
}
