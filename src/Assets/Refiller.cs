using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class Refiller : MonoBehaviour
{
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
}
