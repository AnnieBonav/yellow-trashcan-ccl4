using System;
using UnityEngine;

public class GarbageCan : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Potion")){
            interactionsHandler.RaiseInteraction(InteractionEvents.ThrowPotionGarbage);
            // TODO: Add dissapear sound
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
    }
}
