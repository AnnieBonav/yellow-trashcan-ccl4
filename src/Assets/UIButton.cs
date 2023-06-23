using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes {  MainMenu, Tutorial, Level, Settings, Credits }
public class UIButton : MonoBehaviour
{
    public static event Action<bool> GoIngame; // Bool is whether to do tutorial or not
    [SerializeField] private GameScenes sceneToGo;

    public void StartLevel()
    {
        print("Start level from menu");
    }

    public void ChangeScene()
    {
        print("Asked to change Scene to" +  sceneToGo);
        switch(sceneToGo)
        {
            case GameScenes.MainMenu:
                SceneManager.LoadSceneAsync("MainMenu");
                break;
            case GameScenes.Tutorial:
                GoIngame?.Invoke(true);
                SceneManager.LoadSceneAsync("Ingame");
                break; // How do i tell it to open the tutorial and not the level?
            case GameScenes.Level:
                GoIngame?.Invoke(false);
                SceneManager.LoadSceneAsync("Ingame");
                break;
            case GameScenes.Settings:
                SceneManager.LoadSceneAsync("Settings");
                break;
        }
    }
}
