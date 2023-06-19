using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Refiller : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

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
            InteractionRaised?.Invoke(InteractionEvents.RefilledLiquid);
        }
        else
        {
            InteractionRaised?.Invoke(InteractionEvents.RefilledSolid);
        }
    }
}
