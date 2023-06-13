using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionSystemTest : MonoBehaviour
{
    [SerializeField] private IngredientAcceptor acceptor;
    [SerializeField] private Ingredient prefab;

    public void AddIngredient1()
    {
        AddIngredient(IngredientType.Placeholder1, Color.blue);
    }

    public void AddIngredient2()
    {
        AddIngredient(IngredientType.Placeholder2, Color.red);
    }
    
    public void AddIngredient(IngredientType type, Color color)
    {
        Debug.Log("button pressed");
        Ingredient noob = Instantiate(prefab);
        Debug.Log(noob);
        noob.IngredientType = type;
        noob.ColourModifier = color;
        acceptor.Take(noob);
    }

}
