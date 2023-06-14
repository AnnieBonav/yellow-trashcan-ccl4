using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGrabbable : MonoBehaviour
{
    private IngredientContainer ingredientContainer;

    private void Awake()
    {
        print("I am awaking");
        ingredientContainer = gameObject.GetComponentInParent<IngredientContainer>();
    }

    public void MakeIngredientChild()
    {
        Ingredient noobIngredient = ingredientContainer.TakeIngredient();
        noobIngredient.transform.SetParent(transform); // Why does it not change the parent if I do it here, but it does if I do it in another function?
    }

    public void RemoveFromContainer()
    {
        gameObject.transform.SetParent(null);
        ingredientContainer.ResetEmptyIngredient();
    }

    public void DestroyContainer()
    {
        //ingredientContainer.ResetEmptyIngredient();
        //Destroy(gameObject);
    }
}
