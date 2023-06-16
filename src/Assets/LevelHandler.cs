using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private float levelDurationSeconds = 10;
    [SerializeField] private CharactersController characterController;
    [SerializeField] private Dialogue dialogue;

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

    public void StartLevel()
    {
        print("The level has officially started!");
        characterController.SetToLevelPosition();

    }

    private IEnumerator LevelTimer()
    {
        print("Level timer has started");
        yield return levelTimer;
        print("level timer has ended");
    }
}
