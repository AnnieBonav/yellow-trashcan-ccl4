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
    private IngredientSpawner _spawner;

    private void Awake()
    {
        Refill();
        _spawner = GetComponent<IngredientSpawner>();
    }

    public void Refill()
    {
        currentCapacity = maximumCapacity;
        UpdateFillDisplayText();
    }

    public Ingredient TakeIngredient()
    {
        if (currentCapacity <= 0) return null;
        currentCapacity--;
        UpdateFillDisplayText();
        return _spawner.SpawnIngredient();
    }

    private void UpdateFillDisplayText()
    {
        if (fillDisplay is not null) fillDisplay.text = $"{currentCapacity}/{maximumCapacity}";
    }

}
