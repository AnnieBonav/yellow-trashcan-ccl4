using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskContainer : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPositions;
    [SerializeField] private int maximumCapacity = 2;
    [SerializeField] private int currentCapacity;
    [SerializeField] private GameObject flaskPrefab;

    private void Awake()
    {
        GarbageCan.InteractionRaised += HandleRaisedInteractions;
        Customer.InteractionRaised += HandleRaisedInteractions;
        if (spawnPositions.Count > 0)
        {
            maximumCapacity = spawnPositions.Count;
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            RefillSlot();
        }

        Refill();
    }

    private void OnDisable()
    {
        GarbageCan.InteractionRaised -= HandleRaisedInteractions;
        Customer.InteractionRaised -= HandleRaisedInteractions;
    }

    private void HandleRaisedInteractions(InteractionEvents raisedEvent)
    {
        print("Handling brew interaction");
        if(raisedEvent == InteractionEvents.ThrowPotionGarbage || raisedEvent == InteractionEvents.DeliverCorrectPotion)
        {
            print("Threw potion garbage or delivered correct potion");
            RefillSlot();
        }
    }

    private void RefillSlot()
    {
        GameObject noobFlask = Instantiate(flaskPrefab);
        Flask flask = noobFlask.GetComponent<Flask>();

        foreach (GameObject spawnPosition in spawnPositions)
        {
            if (spawnPosition.transform.childCount <= 0)
            {
                noobFlask.transform.SetParent(spawnPosition.transform, false);
                noobFlask.transform.position = spawnPosition.transform.position;

                return;
            }
        }
    }

    public void RefillSingle()
    {
        print("Current capacity: " + currentCapacity + " Max capacity: " + maximumCapacity);
        if (currentCapacity != maximumCapacity)
        {
            RefillSlot();
            currentCapacity--;
        }
        else
        {
            print("You have refilled max!");
        }
    }
    public void Refill()
    {
        currentCapacity = maximumCapacity;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Ingredient")) // If they have the same parent, then the ingredient should be unparented because it is taken away
        {
            currentCapacity--;
            print("An ingredient left. Current capacity: " + currentCapacity);
        }
    }
}
