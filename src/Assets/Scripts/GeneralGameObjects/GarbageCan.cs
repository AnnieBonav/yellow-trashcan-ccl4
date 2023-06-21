using System;
using UnityEngine;

public class GarbageCan : MonoBehaviour
{
    [SerializeField] private InteractionsHandler interactionsHandler;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Potion")){
            print("Putting Potion in garbage");
            interactionsHandler.RaiseInteraction(InteractionEvents.ThrowPotionGarbage);
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
    }
}
