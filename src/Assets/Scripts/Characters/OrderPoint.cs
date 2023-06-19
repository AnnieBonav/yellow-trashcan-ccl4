using System;
using System.Collections;
using UnityEngine;

public class OrderPoint : MonoBehaviour
{
    public static event Action<int> CustomerArrived;

    public int pointNumber;
    public bool isOccupied;
    public GameObject whichCustomerIsHere; // Add it so even if other custoemrs go through it, we do not care about them leaving, 

    private void OnTriggerEnter(Collider collider)
    {
        // if (isOccupied) return; // Do not care what happens when it is opccupied
        if (collider.CompareTag("Customer"))
        {
            print("A customer arrived to the order point");
            CustomerArrived?.Invoke(pointNumber);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        print(collider.transform.parent.name + " vs  " + whichCustomerIsHere.name);
        // Scary ghost code collider.transform.parent == whichCustomerIsHere
        if (collider.CompareTag("Customer"))
        {
            isOccupied = false;
            // StopAllCoroutines();
            // StartCoroutine(SetUnoccupied());
        }
    }

    private IEnumerator SetUnoccupied()
    {
        yield return new WaitForSeconds(3);
        isOccupied = false;
    }
}
