using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSign : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InteractionsHandler interactionsHandler;

    private CurrentRoom currentRoom = CurrentRoom.Entrance;
    private bool canActivateSign = false;
    private void Awake()
    {
        Dialogue.AskToActivateDoor += ActivateSign;
    }

    private void ActivateSign(CurrentRoom commingCurrentRoom) // COPIED CODE from door
    {
        if (currentRoom == commingCurrentRoom)
        {
            canActivateSign = true;
            animator.SetBool("IsIdling", canActivateSign);
        }
    }

    public void TurnSign()
    {
        if (!canActivateSign) return;
        StartCoroutine(ActivateSign());
    }

    private IEnumerator ActivateSign()
    {
        animator.SetTrigger("TurnSign");
        yield return new WaitForSeconds(3f);
        interactionsHandler.RaiseInteraction(InteractionEvents.TravelledBrewing);
    }
}
