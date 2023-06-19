using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCandle : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    public void TriggerMagicVision()
    {
        print("Magic vision started");
        InteractionRaised?.Invoke(InteractionEvents.RevealIngredients);
    }
}
