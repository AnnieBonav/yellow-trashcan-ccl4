using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCandle : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    public void TriggerMagicVision()
    {
        print("Magic vision started");
        interactionsHandler.RaiseInteraction(InteractionEvents.RevealIngredients);
    }
}
