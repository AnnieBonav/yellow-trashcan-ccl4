using System;
using UnityEngine;

public class EmptyGrabbable : MonoBehaviour
{
    public static event Action<bool> HoverBarkContainer;
    [SerializeField] private Collider emptyCollider;
    private IngredientContainer ingredientContainer;

    private void Awake()
    {
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

    public void ChangeIsTrigger()
    {
        emptyCollider.isTrigger = true;
    }

    public void DestroyContainer()
    {
        //ingredientContainer.ResetEmptyIngredient();
        //Destroy(gameObject);
    }

    public void HoverBark()
    {
        HoverBarkContainer?.Invoke(true);
    }

    public void UnhoverBark()
    {
        HoverBarkContainer?.Invoke(false);
    }
}
