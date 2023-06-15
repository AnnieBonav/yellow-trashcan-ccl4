using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentRoom { Garden, Brewing, Entrance }
public enum InteractionEvents { GrabIngredient, ReleaseIngredient, GrabPotion, ReleasePotion, OpenDoor, DeliverCorrectPotion, DeliverIncorrectPotion }

public class TestFlags : MonoBehaviour
{

    public static event Action<InteractionEvents> InteractionRaised;
    private void OnTriggerExit(Collider collider)
    {
        print(collider.name + " Exited");
        InteractionRaised?.Invoke(InteractionEvents.GrabIngredient);
    }
}
