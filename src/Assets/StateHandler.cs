using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public static StateHandler Instance;
    private bool startWithTutorial;
    public bool StartWithTutorial
    {
        get { return startWithTutorial; }
        set { startWithTutorial = value;}
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
