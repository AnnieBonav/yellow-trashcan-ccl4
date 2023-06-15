using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIngredientBox : MonoBehaviour
{
    [SerializeField] private IngredientContainer container;

    public void SpawnIngredient()
    {
        container.TakeIngredient();
        //tmp.transform.Translate(2, 3, 2);
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3);
        /* var tmp = container.TakeIngredient();
        tmp.transform.Translate(2, 3, 2);
        Debug.Log(tmp);
        yield return new WaitForSeconds(3);
        tmp = container.TakeIngredient();
        tmp.transform.Translate(2, 5, 2);
        Debug.Log(tmp);
        yield return new WaitForSeconds(3);
        tmp = container.TakeIngredient();
        tmp.transform.Translate(2, 7, 2);
        Debug.Log(tmp);
        yield return new WaitForSeconds(3);
        Debug.Log(container.TakeIngredient());
        container.Refill();
        yield return new WaitForSeconds(3);
        tmp = container.TakeIngredient();
        tmp.transform.Translate(2, 8, 2);
        Debug.Log(tmp);*/
    }
}
