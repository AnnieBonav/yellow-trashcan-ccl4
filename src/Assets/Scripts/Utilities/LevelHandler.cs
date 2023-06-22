using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WinningCondition { TimeLimit, CustomersLimit }
public class LevelHandler : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    [SerializeField] private bool isDebugging = false;

    [Header("Scene components")]
    [SerializeField] private CharactersController charactersController;
    [SerializeField] private CustomerSpawner customerSpawner;

    [Header("UI")]
    [Tooltip("This would be the opening and closing pause Menu action")]
    [SerializeField] private InputActionReference _interactPauseMenu;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject startDayButton;

    private CurrentRoom currentRoom;
    
    private bool menuOpened = false;

    [SerializeField] private CurrentRoom startRoom;

    [Header("Winning condition")]
    [Tooltip("Whether the level will end after a certain time or number of customers that have appeared.")]
    [SerializeField] WinningCondition winningCondition = WinningCondition.CustomersLimit;

    [Tooltip("How long the level will last. AFter this time the game ends.")]
    [SerializeField] private float levelDurationSeconds = 10;
    private WaitForSeconds levelTimer;

    [Tooltip("How many customers will appear in the whole level. When this amount is reached, the level ends.")]
    [SerializeField] private int customersTargetAmount;
    private int currentAmountOfCustomers = 0; // How many customers have been served (correct or wrong, does not matter)

    private void Awake()
    {
        startDayButton.SetActive(false);
        if(winningCondition == WinningCondition.TimeLimit && levelDurationSeconds <= 10)
        {
            levelDurationSeconds = 10;
            if (isDebugging) print("The level will not last less than 10 seconds.");
        }

        levelTimer = new WaitForSeconds(levelDurationSeconds);

        var interactPauseAction = GetInputAction(_interactPauseMenu);
        interactPauseAction.canceled += HandlePauseMenu;

        InteractionsHandler.InteractionRaised += ChangeRoom;
        Dialogue.InteractionRaised += HandleInteractionRaised;
        CustomerSpawner.SpawnedCustomer += HandleSpawnedCustomer;
        currentRoom = startRoom;
    }

    private void OnDisable()
    {
        InteractionsHandler.InteractionRaised -= ChangeRoom;
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        charactersController.PositionCharacters(currentRoom);
        AkSoundEngine.SetState("CurrentRoom", currentRoom.ToString());
    }

    private void HandleSpawnedCustomer()
    {
        if (isDebugging) print("Handling customers spawning in Level Handler.");
        currentAmountOfCustomers++;
        if(isDebugging) print("New amount of spawned customers: " + currentAmountOfCustomers);
        if (winningCondition != WinningCondition.CustomersLimit) return;

        if (isDebugging) print("The type of winning is amount of customers spawned, so it will be checked.");
        if (isDebugging) print("Spawned customers: " + currentAmountOfCustomers + "  Number to reach: " + customersTargetAmount);
        if (currentAmountOfCustomers >= customersTargetAmount)
        {
            print("The max amount has been reached on the level handler, it will ask the custoemr spawner to stop spawning custoemrs.");
            customerSpawner.StopSpawningCustomers();
        }
    }

    private void OpenStartDayButton()
    {
        print("The level handler is opening start day button");
        charactersController.RemoveBook();
        startDayButton.SetActive(true);
    }

    public void ChangeRoom(InteractionEvents interactionEvent)
    {
        if (!(interactionEvent == InteractionEvents.TravelledEntrance || interactionEvent == InteractionEvents.TravelledBrewing || interactionEvent == InteractionEvents.TravelledGarden)) return;
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
            interactionsHandler.RaiseInteraction(InteractionEvents.ResumeGame);
        }
        else
        {
            pauseMenu.SetActive(true);
            menuOpened = true;
            interactionsHandler.RaiseInteraction(InteractionEvents.PauseGame);
        }
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }

    private void HandleInteractionRaised(InteractionEvents interactionRaised)
    {
        switch (interactionRaised)
        {
            case InteractionEvents.FinishedTutorial:
                print("Will start level because tutorial was finished");
                OpenStartDayButton();
                break;
        }
    }
    public void StartLevel()
    {
        interactionsHandler.RaiseInteraction(InteractionEvents.LevelStarted);
        startDayButton.SetActive(false);

        switch (winningCondition)
        {
            case WinningCondition.TimeLimit:
                StartCoroutine(LevelTimer());
                if (isDebugging) print("The level has officially started! The winning condition is Time. The level will last " + levelDurationSeconds + " seconds.");
                break;
            case WinningCondition.CustomersLimit:
                if (isDebugging) print("The level has officially started! The winning condition is amount of customers. The level will end after " + customersTargetAmount + " customers have appeared.");
                break;
        }
        
        charactersController.SetToLevelPosition();
        customerSpawner.SpawnCustomer();
    }

    private IEnumerator LevelTimer()
    {
        yield return levelTimer;
        if (isDebugging) print("level timer has ended.");
        interactionsHandler.RaiseInteraction(InteractionEvents.LevelEnded);
    }
}
