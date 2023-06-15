using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IngredientSpawner))]
public class IngredientContainer : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPositions;
    [SerializeField] private int maximumCapacity = 3;
    [SerializeField] private int currentCapacity;
    [SerializeField] private TextMeshProUGUI fillDisplay;

    [SerializeField] private bool needsEmpty;
    [SerializeField] private GameObject emptyIngredient;

    private IngredientSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<IngredientSpawner>();

        if (spawnPositions.Count > 0)
        {
            maximumCapacity = spawnPositions.Count;
        }

        for(int i = 0; i < spawnPositions.Count; i++)
        {
            RefillSlot();
        }

        Refill();
    }

    private void Start()
    {
        if(needsEmpty) ResetEmptyIngredient(); // Only bark
    }

    private void RefillSlot()
    {
        Ingredient noobIngredient = _spawner.SpawnIngredient();
        foreach(GameObject spawnPosition in spawnPositions)
        {
            if(spawnPosition.transform.childCount <= 0)
            {
                noobIngredient.gameObject.transform.SetParent(spawnPosition.transform, false);
                noobIngredient.transform.position = spawnPosition.transform.position;

                return;
            }
        }
    }

    public void RefillSingle()
    {
        print("Current capacity: " + currentCapacity + " Max capacity: " + maximumCapacity);
        if(currentCapacity != maximumCapacity)
        {
            RefillSlot();
            currentCapacity--;
            print("Refilled");
        }
        else
        {
            print("You have refilled max!");
        }
    }
    public void Refill()
    {
        currentCapacity = maximumCapacity;
        UpdateFillDisplayText();
    }

    public Ingredient TakeIngredient()
    {
        if (currentCapacity < 0)
        {
            print("There are no more ingredients");
            return null;
        }
        currentCapacity--;
        UpdateFillDisplayText();

        Ingredient noobIngredient = _spawner.SpawnIngredient();
        print("Returning new ingredient");
        return noobIngredient;
    }

    public void ResetEmptyIngredient()
    {
        Instantiate(emptyIngredient, transform);
        print("Creating new container");
    }

    private void UpdateFillDisplayText()
    {
        if (fillDisplay is not null) fillDisplay.text = $"{currentCapacity}/{maximumCapacity}";
    }

    /* Mess with parents
        GameObject parent = collider.transform.parent.gameObject;
        GameObject grandParent = parent.transform.parent.gameObject;
        GameObject greatGrandParent = grandParent.transform.parent.gameObject;
        GameObject greatGreatGrandParent = greatGrandParent.transform.parent.gameObject;
        GameObject superParent = collider.transform.parent.parent.parent.parent.gameObject; // This will be the parent container

    */

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Ingredient")) // If they have the same parent, then the ingredient should be unparented because it is taken away
        {
            currentCapacity--;
            print("An ingredient left. Current capacity: " + currentCapacity);
        }
    }

}
