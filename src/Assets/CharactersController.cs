using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform book;

    [Header("Positions Book")]
    [SerializeField] private Transform bookEntrance;
    [SerializeField] private Transform bookBrewing;
    [SerializeField] private Transform bookGarden;

    [Header("Positions Player")]
    [SerializeField] private Transform playerEntrance;
    [SerializeField] private Transform playerBrewing;
    [SerializeField] private Transform playerGarden;

    private void Awake()
    {
        Door.InteractionRaised += ChangePosition;
    }

    private void Start()
    {
        player.position = playerEntrance.position;
        book.position = bookEntrance.position;
    }

    public void ChangePosition(InteractionEvents interactionEvent)
    {
        switch (interactionEvent)
        {
            case InteractionEvents.TravelledEntrance:
                player.position = playerEntrance.position;
                book.position = bookEntrance.position;

                book.LookAt(player);
                break;

            case InteractionEvents.TravelledBrewing:
                player.position = playerBrewing.position;
                book.position = bookBrewing.position;

                book.LookAt(player);
                break;

            case InteractionEvents.TravelledGarden:
                player.position = playerGarden.position;
                book.position = bookGarden.position;

                book.LookAt(player);
                break;
        }
        
    }
}
