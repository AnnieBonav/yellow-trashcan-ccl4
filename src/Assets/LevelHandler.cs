using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private float levelDurationSeconds = 10;
    [SerializeField] private CharactersController characterController;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private bool isDebugging = false;

    private WaitForSeconds levelTimer;

    private void Awake()
    {
        if(levelDurationSeconds <= 10)
        {
            levelDurationSeconds = 10;
            print("The level will not last less than 10 seconds.");
        }

        levelTimer = new WaitForSeconds(levelDurationSeconds);
    }

    private void Start()
    {
        if (isDebugging)
        {
            print("WIll start level");
            StartLevel();
        }
    }

    public void StartLevel()
    {
        StartCoroutine(LevelTimer());
        print("The level has officially started!");
        characterController.SetToLevelPosition();
        customerSpawner.SpawnCustomer();
    }

    private IEnumerator LevelTimer()
    {
        print("Level timer has started");
        yield return levelTimer;
        print("level timer has ended");
    }
}
