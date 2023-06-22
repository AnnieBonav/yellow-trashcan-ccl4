using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    [SerializeField] private RecipeData type;

    public RecipeData Type => type;

    public void GrabPotion()
    {
        interactionsHandler.RaiseInteraction(InteractionEvents.GrabPotion);
    }

    public void ReleasePotion()
    {
        interactionsHandler.RaiseInteraction(InteractionEvents.ReleasePotion);
    }
}
