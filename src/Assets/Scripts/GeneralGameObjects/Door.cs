using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    [SerializeField] private CurrentRoom currentRoom;
    [SerializeField] private CurrentRoom roomToGo;
    [SerializeField] private Animator animator;

    [Header("Active/unactive settings")]
    [SerializeField] private MeshRenderer handleMesh;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material hoveredMaterial;

    private bool canActivateDoor;

    private void Awake()
    {
        Dialogue.AskToActivateDoor += ActivateDoor;
        LevelHandler.AskToActivateDoor += ActivateDoor;
    }
    public void ChangeScenery()
    {
        if (!canActivateDoor) return; // Cannot activate if it is not the time to do so
        print("Wants to go to room");
        switch (roomToGo)
        {
            case CurrentRoom.Garden:
                interactionsHandler.RaiseInteraction(InteractionEvents.TravelledGarden);
                break;
            case CurrentRoom.Brewing:
                interactionsHandler.RaiseInteraction(InteractionEvents.TravelledBrewing);
                break;
            case CurrentRoom.Entrance:
                interactionsHandler.RaiseInteraction(InteractionEvents.TravelledEntrance);
                break;
        }

        AkSoundEngine.SetState("CurrentRoom", roomToGo.ToString());
    }

    private void ActivateDoor(CurrentRoom commingCurrentRoom)
    {
        if(currentRoom == commingCurrentRoom)
        {
            canActivateDoor = true;
        }
    }

    public void HoverDoor()
    {
        if (!canActivateDoor) return;
        handleMesh.material = hoveredMaterial;
    }

    public void UnhoverDoor()
    {
        if (!canActivateDoor) return;
        handleMesh.material = activeMaterial;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Customer")){
            TriggerOpenDoor();
        }
    }

    private void TriggerOpenDoor()
    {
        animator.SetTrigger("OpenDoor");
        AkSoundEngine.PostEvent("Play_Door", gameObject);
    }
}
