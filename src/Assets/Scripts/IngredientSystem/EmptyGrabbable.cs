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
        noobIngredient.transform.SetParent(transform);
    }

    public void DestroyContainer()
    {
        ingredientContainer.ResetEmptyIngredient();
        Destroy(gameObject);
    }
}
