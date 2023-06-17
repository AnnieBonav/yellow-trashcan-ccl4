using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelHandler : MonoBehaviour
{
    private CurrentRoom currentRoom;
    [SerializeField] private float levelDurationSeconds = 10;
    [SerializeField] private CharactersController charactersController;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private CustomerSpawner customerSpawner;

    [SerializeField] private bool isDebugging = false;

    private WaitForSeconds levelTimer;

    [Header("UI")]
    [SerializeField]
    [Tooltip("This would be the opening and closing pause Menu action")]
    InputActionReference _interactPauseMenu;
    [SerializeField] private GameObject pauseMenu;    

    private bool menuOpened = false;

    [SerializeField] private CurrentRoom startRoom;

    private void Awake()
    {
        if(levelDurationSeconds <= 10)
        {
            levelDurationSeconds = 10;
            print("The level will not last less than 10 seconds.");
        }

        levelTimer = new WaitForSeconds(levelDurationSeconds);

        var interactPauseAction = GetInputAction(_interactPauseMenu);
        interactPauseAction.canceled += HandlePauseMenu;

        Door.InteractionRaised += ChangeRoom;
        currentRoom = startRoom;
    }

    private void Start()
    {
        pauseMenu.SetActive(false);

        if (isDebugging)
        {
            StartLevel();
        }
        charactersController.PositionCharacters(currentRoom);
    }

    public void ChangeRoom(InteractionEvents interactionEvent)
    {
        switch (interactionEvent)
        {
            case InteractionEvents.TravelledEntrance:
                currentRoom = CurrentRoom.Entrance;
                break;

            case InteractionEvents.TravelledBrewing:
                currentRoom = CurrentRoom.Brewing;
                break;

            case InteractionEvents.TravelledGarden:
                currentRoom = CurrentRoom.Garden;
                break;
        }

        charactersController.PositionCharacters(currentRoom);
    }

    
    private void HandlePauseMenu(InputAction.CallbackContext actionReference)
    {
        if (menuOpened)
        {
            pauseMenu.SetActive(false);
            menuOpened = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            menuOpened = true;
        }
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }


    public void StartLevel()
    {
        StartCoroutine(LevelTimer());
        print("The level has officially started!");
        charactersController.SetToLevelPosition();
        customerSpawner.SpawnCustomer();
    }

    private IEnumerator LevelTimer()
    {
        yield return levelTimer;
        print("level timer has ended.");
    }
}
