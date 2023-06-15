using System;
using UnityEngine;

public class GarbageCan : MonoBehaviour
{
    public static event Action<InteractionEvents> InteractionRaised;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Potion")){
            print("Putting Potion in garbage");
            InteractionRaised?.Invoke(InteractionEvents.ThrowPotionGarbage);
            Destroy(collider.gameObject.transform.parent.gameObject);
        }
    }
}
