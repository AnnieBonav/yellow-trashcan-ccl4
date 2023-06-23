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
            CustomerArrived?.Invoke(pointNumber);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Customer"))
        {
            isOccupied = false;
        }
    }
}
