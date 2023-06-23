using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform book;
    [SerializeField] private Transform uiPause;

    [Header("Positions Book")]
    [SerializeField] private Transform bookEntrance;
    [SerializeField] private Transform bookBrewing;
    [SerializeField] private Transform bookGarden;

    [Header("Positions Player")]
    [SerializeField] private Transform playerEntrance;
    [SerializeField] private Transform playerBrewing;
    [SerializeField] private Transform playerGarden;


    [Header("Positions UI")]
    [SerializeField] private Transform uiBrewing;
    [SerializeField] private Transform uiGarden;
    [SerializeField] private Transform uiEntrance;

    public void SetToLevelPosition()
    {
        player.position = playerBrewing.position;
        uiPause.position = uiBrewing.position;
        book.position = bookBrewing.position;
        book.gameObject.SetActive(false);
    }

    public void RemoveBook()
    {
        book.gameObject.SetActive(false);
    }
    public void PositionCharacters(CurrentRoom currentRoom)
    {
        switch (currentRoom)
        {
            case CurrentRoom.Entrance:
                player.position = playerEntrance.position;
                book.position = bookEntrance.position;
                uiPause.position = uiEntrance.position;

                player.LookAt(playerEntrance.forward);
                break;

            case CurrentRoom.Brewing:
                player.position = playerBrewing.position;
                book.position = bookBrewing.position;
                uiPause.position = uiBrewing.position;

                player.LookAt(playerBrewing.forward);
                break;

            case CurrentRoom.Garden:
                player.position = playerGarden.position;
                book.position = bookGarden.position;
                uiPause.position = uiGarden.position;

                player.LookAt(playerGarden.forward);
                break;
        }

        // ROTATE
        book.LookAt(player);
        uiPause.LookAt(player);
        uiPause.rotation = Quaternion.Euler(0, uiPause.rotation.eulerAngles.y, 0);
    }
}
