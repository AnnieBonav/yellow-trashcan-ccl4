using UnityEngine;
using TMPro;

public class BookoFacade : MonoBehaviour
{
    public GameObject BookoCharacter;
    public GameObject BookoUI;
    public TextMeshProUGUI DialogueText;
    public Animator BookoAnimator;
    public GameObject ContinueButton;
    public GameObject GrabTip;
    public GameObject TriggerTip;
    public GameObject ATip;
    public GameObject BTip;

    public void DeactivateTips()
    {
        if (GrabTip != null) GrabTip.SetActive(false);
        if (TriggerTip != null) TriggerTip.SetActive(false);
        if (ATip != null) ATip.SetActive(false);
        if(BTip != null ) BTip.SetActive(false);
    }
}
