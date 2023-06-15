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



    private void Start()
    {
        player.position = playerEntrance.position;
        book.position = bookEntrance.position;
    }

    public void ChangePosition(CurrentRoom newRoom)
    {
        switch (newRoom)
        {
            case CurrentRoom.Entrance:
                player.position = playerEntrance.position;
                book.position = bookEntrance.position;

                book.LookAt(player);
                break;

            case CurrentRoom.Brewing:
                player.position = playerBrewing.position;
                book.position = bookBrewing.position;

                book.LookAt(player);
                break;

            case CurrentRoom.Garden:
                player.position = playerGarden.position;
                book.position = bookGarden.position;

                book.LookAt(player);
                break;
        }
        
    }
}
