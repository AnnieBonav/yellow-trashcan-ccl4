using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    [SerializeField] private MeshRenderer handleMesh;
    [SerializeField] private CurrentRoom roomToGo;

    private bool canActivateDoor;
    public void ChangeScenery()
    {
        if (!canActivateDoor) return; // Cannot activate if it is not the time to do so
        switch (roomToGo)
        {
            case CurrentRoom.Garden:
                InteractionRaised?.Invoke(InteractionEvents.TravelledGarden);
                break;
            case CurrentRoom.Brewing:
                InteractionRaised?.Invoke(InteractionEvents.TravelledBrewing);
                break;
            case CurrentRoom.Entrance:
                InteractionRaised?.Invoke(InteractionEvents.TravelledEntrance);
                break;
        }
    }

    public void CanActivateDoor(bool canActivate)
    {
        canActivateDoor = canActivate;
    }

    // TODO maybe make the can activate/deactivate better
    public void ActivateDoor()
    {
        if(canActivateDoor) handleMesh.material.color = Color.green;
    }

    public void DeactivateDoor()
    {
        if (canActivateDoor) handleMesh.material.color= Color.white;
    }
}
