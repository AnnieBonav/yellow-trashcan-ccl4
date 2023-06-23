using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public static StateHandler Instance;
    private bool startWithTutorial;
    public bool StartWithTutorial => startWithTutorial;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            UIButton.GoIngame += HandleIngameDecision;
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
