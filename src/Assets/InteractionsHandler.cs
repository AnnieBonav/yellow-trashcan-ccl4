using System;
using UnityEngine;

public class InteractionsHandler : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    public void RaiseInteraction(InteractionEvents interactionType)
    {
        InteractionRaised?.Invoke(interactionType);
    }
}
