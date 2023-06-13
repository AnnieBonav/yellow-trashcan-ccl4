using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform orderPoint;
    [SerializeField] private List<Customer> customerPrefabs;

    void Start()
    {
        if (orderPoint is not null) StartCoroutine(SpawnCustomerAfterSeconds(3));
    }

    private IEnumerator SpawnCustomerAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            Customer noob = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position,
            Quaternion.identity);
            noob.StartCustomerBehaviour(orderPoint.position);
            yield return new WaitForSeconds(10);
        }
        
    }
}
