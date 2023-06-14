using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IngredientSpawner))]
public class IngredientContainer : MonoBehaviour
{
    [SerializeField] private int maximumCapacity = 3;
    [SerializeField] private int currentCapacity;
    [SerializeField] private TextMeshProUGUI fillDisplay;
    [SerializeField] private GameObject emptyIngredient;

    private IngredientSpawner _spawner;

    private void Awake()
    {
        Refill();
        _spawner = GetComponent<IngredientSpawner>();
    }

    private void Start()
    {
        ResetEmptyIngredient();
    }

    public void Refill()
    {
        currentCapacity = maximumCapacity;
        UpdateFillDisplayText();
    }

    public Ingredient TakeIngredient()
    {
        if (currentCapacity <= 0)
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

}
