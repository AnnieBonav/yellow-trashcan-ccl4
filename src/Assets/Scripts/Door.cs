using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;

    [SerializeField] private Transform roomPosition;
    [SerializeField] private Transform gardenPosition;
    [SerializeField] private Transform entrancePosition;

    [SerializeField] private GameObject player;
    [SerializeField] private MeshRenderer handleMesh;

    [SerializeField] private CurrentRoom roomToGo;

    public void ChangeScenery()
    {
        switch (roomToGo)
        {
            case CurrentRoom.Garden:
                player.transform.position = gardenPosition.position;
                InteractionRaised?.Invoke(InteractionEvents.TravelledGarden);
                break;
            case CurrentRoom.Brewing:
                player.transform.position = roomPosition.position;
                InteractionRaised?.Invoke(InteractionEvents.TravelledBrewing);
                break;
            case CurrentRoom.Entrance:
                player.transform.position = entrancePosition.position;
                InteractionRaised?.Invoke(InteractionEvents.TravelledEntrance);
                break;
        }
    }

    public void ActivateDoor()
    {
        handleMesh.material.color = Color.green;
    }

    public void DeactivateDoor()
    {
        handleMesh.material.color= Color.white;
    }
}
