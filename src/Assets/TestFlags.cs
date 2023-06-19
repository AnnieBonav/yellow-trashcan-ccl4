using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentRoom { Garden, Brewing, Entrance }
public enum InteractionEvents { GrabIngredient, ReleaseIngredient, PutIngredientPot, RefilledLiquid, RefilledSolid, CreateCorrectPotion, CreateIncorrectPotion, GrabPotion, ReleasePotion, DeliverCorrectPotion, DeliverIncorrectPotion, ThrowPotionGarbage, TravelledGarden, TravelledBrewing, TravelledEntrance, CreatePotion, GrabFlask, CustomerArrives, LevelStarted, LevelEnded, PauseGame, ResumeGame, RevealIngredients}

public class TestFlags : MonoBehaviour
{

    public static event Action<InteractionEvents> InteractionRaised;
    private void OnTriggerExit(Collider collider)
    {
        print(collider.name + " Exited");
        InteractionRaised?.Invoke(InteractionEvents.GrabIngredient);
    }
}
