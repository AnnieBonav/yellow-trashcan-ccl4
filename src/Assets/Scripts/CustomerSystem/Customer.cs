using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour
{
    private enum CustomerState
    {
        Waiting,
        WalkingToOrder,
        Ordered,
        GoAway
    }

    public static event Action<InteractionEvents> InteractionRaised;

    [SerializeField] private float distanceEpsilon = 0.1f;
    [SerializeField] private TextMeshProUGUI orderText;

    private Vector3 _spawnPosition;
    private NavMeshAgent _navMeshAgent;
    private CustomerState _state = CustomerState.Waiting;
    private RecipeData _requestedPotion;
    private IEnumerator _getAngryCoroutine;

    private Vector3 _previousForward;
    private Vector3 _lookAtPosition;
    private float _lookAtTimer = 0;

    private int orderPosition = -1;
    private bool hasLimit = true; // This lets us not have a time limit when in tutorial

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _spawnPosition = transform.position;

        OrderPoint.CustomerArrived += HandleCustomerArrive;
    }

    private void HandleCustomerArrive(int orderPoint)
    {
        print("A customer was said to arrive in: " + orderPoint);
        if (orderPoint == orderPosition)
        {
            print("The customer" + transform.name + " Arrived to its place #" + orderPoint);
            ChangeState();
        }
    }

    public void StartTutorialCustomer(Vector3 orderWorldPosition, Transform lookAt, int whichOrderPosition)
    {
        _state = CustomerState.WalkingToOrder;
        _navMeshAgent.destination = orderWorldPosition;
        _lookAtPosition = lookAt.position;
        orderPosition = whichOrderPosition; // This is the p[lace where they are supposed to go, it should be st by the customer spawner when it gets instanced
        hasLimit = false;
    }


    void Start()
    {
        _requestedPotion = PotionKnowledgebase.Instance.RandomRecipe();
    }

    void ChangeState()
    {
        switch (_state)
        {
            case CustomerState.WalkingToOrder: // If it is walking to order and change state is called, then it should now order
                Order();
                break;
            case CustomerState.Ordered:
                RotateTowardsLookPosition(Time.deltaTime);
                break;
            case CustomerState.GoAway:
                DespawnIfAtSpawn();
                break;
            default:
                break;
        }
    }

    private void RotateTowardsLookPosition(float delta)
    {
        if(_lookAtTimer >= 1) return;
        _lookAtTimer += delta;
        Vector3 directionVector =  new Vector3(_lookAtPosition.x - transform.position.x, 0, _lookAtPosition.z - transform.position.z);
        transform.forward = Vector3.Lerp(_previousForward, directionVector, _lookAtTimer);
    }
    
    public void StartCustomerBehaviour(Vector3 orderPosition, Transform lookAt)
    {
        _state = CustomerState.WalkingToOrder;
        _navMeshAgent.destination = orderPosition;
        _lookAtPosition = lookAt.position;
    }

    private void DespawnIfAtSpawn()
    {
        if(Vector3.Distance(transform.position, _navMeshAgent.destination) <= distanceEpsilon) Destroy(gameObject);
    }

    private void Order() 
    {
        orderText.text = $"Hello I want a {_requestedPotion.name}!";
        orderText.gameObject.SetActive(true);
        _state = CustomerState.Ordered;
        _previousForward = transform.forward;
        ChangeState();

        if (hasLimit)
        {
            print("Get angry is starting cause there is a limit");
            _getAngryCoroutine = AngryInSeconds(10);
            StartCoroutine(_getAngryCoroutine);
            return;
        }        
    }

    public IEnumerator AngryInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        orderText.text = "I am angry, you did not give me the potion in time!";
        yield return new WaitForSeconds(3);
        MoveToDespawn();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Potion") && _state == CustomerState.Ordered)
        {
            Potion potion = other.gameObject.GetComponent<Potion>();
            Give(potion);
            Destroy(other.gameObject);
        }
    }

    private void Give(Potion vial)
    {
        if (vial is null) return;
        if (vial.Type.name == _requestedPotion.name)
        {
            if(hasLimit) StopCoroutine(_getAngryCoroutine);
            orderText.text = "Thank you, exactly what I wanted";
            InteractionRaised?.Invoke(InteractionEvents.DeliverCorrectPotion);
            MoveToDespawn();
        }
        else
        {
            orderText.text = "That is not what I ordered! Try Again?";
            InteractionRaised?.Invoke(InteractionEvents.DeliverIncorrectPotion);
            if (!hasLimit)
            {
                print("This is a tutorial, you can try again");
                return;
            }
        }
    }

    private void MoveToDespawn()
    {
        _navMeshAgent.destination = _spawnPosition;
        _state = CustomerState.GoAway;
    }
}
