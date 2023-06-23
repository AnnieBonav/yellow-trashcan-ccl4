using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes {  MainMenu, Tutorial, Level, Settings, Credits }
public class UIButton : MonoBehaviour
{
    [SerializeField] private GameScenes sceneToGo;
    
    public void ChangeScene()
    {
        print("Asked to change Scene to" +  sceneToGo);
        switch(sceneToGo)
        {
            case GameScenes.MainMenu:
                SceneManager.LoadSceneAsync("MainMenu");
                break;

            case GameScenes.Tutorial:
                StateHandler.Instance.StartWithTutorial = true;
                SceneManager.LoadSceneAsync("Ingame");
                break;

            case GameScenes.Level:
                StateHandler.Instance.StartWithTutorial = false;
                SceneManager.LoadSceneAsync("Ingame");
                break;

            case GameScenes.Settings:
                SceneManager.LoadSceneAsync("Settings");
                break;
        }
    }
}
