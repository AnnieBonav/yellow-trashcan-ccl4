using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingBook : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Positions")]
    [SerializeField] private Transform entrancePosition;
    [SerializeField] private Transform brewingPosition;
    [SerializeField] private Transform gardenPosition;

    private void Awake()
    {
        transform.position = entrancePosition.position;
    }

    // TODO: Make sure to look after the player has moved
    public void ChangePosition(CurrentRoom newRoom)
    {
        switch (newRoom)
        {
            case CurrentRoom.Entrance:
                transform.position = entrancePosition.position;
                transform.LookAt(player);
                break;
            case CurrentRoom.Brewing:
                transform.position = brewingPosition.position;
                transform.LookAt(player);
                break;
            case CurrentRoom.Garden:
                transform.position = gardenPosition.position;
                transform.LookAt(player);
                break;
        }
        
    }
}
