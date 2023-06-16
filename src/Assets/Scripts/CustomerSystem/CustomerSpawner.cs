using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<OrderPoint> orderPoints;
    [SerializeField] private Transform lookAtPoint;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private List<Customer> customerPrefabs;

    [SerializeField] private bool isDebugging;

    private IEnumerator SpawnCustomerAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        Customer noob = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position,
            Quaternion.identity);
        int orderPointIndex = Random.Range(0, orderPoints.Count);
        noob.StartCustomerBehaviour(orderPoints[orderPointIndex].transform.position, lookAtPoint);
    }

    private void Start()
    {
        if(isDebugging) SpawnTutorialCustomer();
    }

    private void SpawnCustomer()
    {
        
    }

    public void SpawnTutorialCustomer()
    {
        print("Spawning Tutorial customer");
        int orderPosition = 1; // I decided so that it goes to the middle
        Customer noob = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position, Quaternion.identity);
        noob.StartTutorialCustomer(orderPoints[orderPosition].transform.position, lookAtPoint, orderPosition, exitPoint.position);
    }

    public void StartSpawningCustomers()
    {
        if (orderPoints.Count > 0)
        {
            print("Start spawning normal customers");
            StartCoroutine(SpawnCustomerAfterSeconds(3));
        }
    }
}
