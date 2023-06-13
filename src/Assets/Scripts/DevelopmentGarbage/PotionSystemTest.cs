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
        AddIngredient(IngredientType.Mushroom, Color.blue, 0.1f, 15f);
    }

    public void AddIngredient2()
    {
        AddIngredient(IngredientType.Liquid, Color.red, 0.2f, -25f);
    }
    
    public void AddIngredient(IngredientType type, Color color, float intensity, float swirl)
    {
        Debug.Log("button pressed");
        Ingredient noob = Instantiate(prefab);
        Debug.Log(noob);
        noob.IngredientType = type;
        noob.ColourModifier = color;
        noob.IntensityModifier = intensity;
        noob.SwirlModifier = swirl;
        acceptor.Take(noob);
    }

}
