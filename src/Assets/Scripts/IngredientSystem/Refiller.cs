using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Refiller : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    [SerializeField] private IngredientContainer ingredientContainer;
    [SerializeField] private bool refillsContainer;

    public void RefillContainer()
    {
        if (refillsContainer)
        {
            ingredientContainer.Refill(); // Basically if it refills bark
        }
        else
        {
            ingredientContainer.RefillSingle();
        }
        
    }

    public void InteractedRefiller(bool isLiquid)
    {
        print("Refilled liquid");
        if (isLiquid)
        {
            interactionsHandler.RaiseInteraction(InteractionEvents.RefilledLiquid);
        }
        else
        {
            interactionsHandler.RaiseInteraction(InteractionEvents.RefilledSolid);
        }
    }
}
