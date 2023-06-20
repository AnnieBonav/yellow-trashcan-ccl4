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
        print("Setting to level position");
        player.position = playerBrewing.position;
        book.position = bookBrewing.position;
        book.gameObject.SetActive(false);
    }

    public void RemoveBook()
    {
        book.gameObject.SetActive(false);
    }
    public void PositionCharacters(CurrentRoom currentRoom)
    {
        print("Sent room to characters: " + currentRoom);
        switch (currentRoom)
        {
            case CurrentRoom.Entrance:
                player.position = playerEntrance.position;
                book.position = bookEntrance.position;

                uiPause.transform.position = uiEntrance.transform.position;
                uiPause.transform.LookAt(uiEntrance.transform.forward * -1);
                break;

            case CurrentRoom.Brewing:
                player.position = playerBrewing.position;
                book.position = bookBrewing.position;

                uiPause.transform.position = uiBrewing.transform.position;
                uiPause.transform.LookAt(uiBrewing.transform.forward * -1);
                break;

            case CurrentRoom.Garden:
                player.position = playerGarden.position;
                book.position = bookGarden.position;

                uiPause.transform.position = uiGarden.transform.position;
                uiPause.transform.LookAt(uiGarden.transform.forward * -1);
                break;
        }

        // ROTATE
        book.LookAt(player);
        uiPause.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);


    }
}
