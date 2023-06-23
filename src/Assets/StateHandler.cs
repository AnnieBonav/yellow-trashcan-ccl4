using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public static StateHandler Instance;
    private bool startWithTutorial;
    public bool StartWithTutorial => startWithTutorial;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            UIButton.GoIngame += HandleIngameDecision;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleIngameDecision(bool choseTutorial)
    {
        if (choseTutorial)
        {
            print("Start with tutorial");
        }
        else
        {
            print("DO NOT Start with tutorial");
        }
    }
}
