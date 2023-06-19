using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    [SerializeField] private CurrentRoom roomToGo;
    [Header("Active/unactive settings")]
    [SerializeField] private MeshRenderer handleMesh;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material unactiveMaterial;

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

    public void ActivateDoor()
    {
        if(canActivateDoor) handleMesh.material = activeMaterial;
    }

    public void DeactivateDoor()
    {
        if (canActivateDoor) handleMesh.material = unactiveMaterial;
    }
}
