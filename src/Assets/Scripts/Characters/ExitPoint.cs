using System;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public static event Action CustomerLeft;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Customer")){
            print("A customer arrived");
            Customer customer = collider.transform.GetComponentInParent<Customer>();
            CustomerLeft?.Invoke();
            Destroy(customer.gameObject);
        }
    }
}
