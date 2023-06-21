using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScenes {  MainMenu, Tutorial, Level, Settings}
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
                SceneManager.LoadSceneAsync("Draft1");
                break; // How do i tell it to open the tutorial and not the level?
            case GameScenes.Level:
                SceneManager.LoadSceneAsync("Draft1");
                break;
            case GameScenes.Settings:
                SceneManager.LoadSceneAsync("Settings");
                break;
        }
    }
}
