using System;
using UnityEngine;

public class OrderPoint : MonoBehaviour
{
    public static event Action<int> CustomerArrived;
    [SerializeField] public int pointNumber;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Customer"))
        {
            print("A customer arrived");
            CustomerArrived?.Invoke(pointNumber);
        }
    }
}
