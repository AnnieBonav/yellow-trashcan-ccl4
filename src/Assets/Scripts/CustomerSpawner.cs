using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
   
    
    [SerializeField] private Transform orderPoint;
    [SerializeField] private List<Customer> customerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        if (orderPoint is not null) StartCoroutine(SpawnCustomerAfterSeconds(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnCustomerAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        Customer noob = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position,
            Quaternion.identity);
        noob.StartCustomerBehaviour(orderPoint.position);
    }
}
