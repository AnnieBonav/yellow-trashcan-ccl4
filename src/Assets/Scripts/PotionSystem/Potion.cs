using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    [SerializeField] private RecipeData type;
    public RecipeData Type => type;

    public void GrabPotion()
    {
        InteractionRaised?.Invoke(InteractionEvents.GrabPotion);
    }

    public void ReleasePotion()
    {
        InteractionRaised?.Invoke(InteractionEvents.ReleasePotion);
    }
}
