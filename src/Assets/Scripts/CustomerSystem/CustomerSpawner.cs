using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<OrderPoint> orderPoints;
    [SerializeField] private Transform lookAtPoint;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private List<GameObject> customerPrefabs;

    [SerializeField] private bool isDebugging;
    [SerializeField] private float timeBetweenCustomers;

    private bool isCooledDown = true; // If it is cooled down, then anotehr custoemr can be spawned
    private bool isWaitingToSpawn = false; // Means that the cooldown went down but there were no free spaces

    private void Awake()
    {
        ExitPoint.CustomerLeft += HandleSpaceFreed;
        Dialogue.AskToSpawnCustomer += SpawnTutorialCustomer;
    }

    private void Start()
    {
        if(isDebugging) SpawnCustomer();
    }

    private void SpawnTutorialCustomer()
    {
        if (isDebugging) print("Spawning Tutorial customer");
        int orderPosition = 0; // Overwritten so it works in scene with only one position
        GameObject noobCustomer = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position, Quaternion.identity);
        Customer customerComponent = noobCustomer.GetComponent<Customer>();
        customerComponent.StartTutorialCustomer(orderPoints[orderPosition].transform.position, lookAtPoint, orderPosition, exitPoint.position);
        orderPoints[orderPosition].isOccupied = true;
        orderPoints[orderPosition].whichCustomerIsHere = noobCustomer;
    }

    public void SpawnCustomer()
    {
        if (!isCooledDown) return;
        if (isDebugging) print("Start spawning normal customers");

        List<int> freeOrderPoints = new List<int>(); // Stores reference to the int of the free order point
        for(int i = 0; i < orderPoints.Count; i++) // Go through all of the points to see which are available
        {
            if (!orderPoints[i].isOccupied)
            {
                freeOrderPoints.Add(i);
            }
        }

        if(freeOrderPoints.Count > 0) // If there are free points then I can spawn one
        {
            int orderPointIndex = Random.Range(0, freeOrderPoints.Count);
            int orderPosition = freeOrderPoints[orderPointIndex];

            GameObject noobCustomer = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], transform.position, Quaternion.identity);
            Customer customerComponent = noobCustomer.GetComponent<Customer>();
            customerComponent.StartCustomerBehaviour(orderPoints[orderPosition].transform.position, lookAtPoint, orderPosition, exitPoint.position); // TODO: can change tp be cleaner and use the same start customer behaviour
            
            orderPoints[orderPosition].isOccupied = true;
            orderPoints[orderPosition].whichCustomerIsHere = noobCustomer;

            StartCoroutine(CoolDownCustomer()); // I have spawned, so I need to start running my cooldown
        }
        else
        {
            if(isDebugging) print("Were no free spaces, so now will be waiting to spawn");
            isWaitingToSpawn = true;
        }
    }

    private void HandleSpaceFreed()
    {
        if (isWaitingToSpawn)
        {
            isWaitingToSpawn = false; // Reset it
            if(isDebugging) print("Was waiting to spawn and now a space has been freed, so another customer can come");
            SpawnCustomer();
        }
    }


    private IEnumerator CoolDownCustomer()
    {
        if(isDebugging) print("Called cool down, so a customer was spawned");
        isCooledDown = false;
        yield return new WaitForSeconds(timeBetweenCustomers);
        isCooledDown = true;
        SpawnCustomer();
    }
}
