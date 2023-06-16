using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        print("A customer arrived");
        if (collider.CompareTag("Customer")){
            Customer customer = collider.transform.GetComponentInParent<Customer>();
            customer.ChangeState();
        }
    }
}
