using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct OrderPoint
{
    public Transform moveTo;
    public Transform lookAt;
}

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<OrderPoint> orderPoints;
    [SerializeField] private List<Customer> customerPrefabs;

    void Start()
    {
        if (orderPoints.Count > 0) StartCoroutine(SpawnCustomerAfterSeconds(3));
    }

    private IEnumerator SpawnCustomerAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        Customer noob = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position,
            Quaternion.identity);
        int orderPointIndex = Random.Range(0, orderPoints.Count);
        noob.StartCustomerBehaviour(orderPoints[orderPointIndex].moveTo.position, orderPoints[orderPointIndex].lookAt.position );
    }
}
