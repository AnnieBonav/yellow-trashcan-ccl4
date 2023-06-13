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
        WalkToOrder,
        Ordered,
        GoAway
    }

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
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _spawnPosition = transform.position;
    }

    void Start()
    {
        _requestedPotion = PotionKnowledgebase.Instance.RandomRecipe();
    }

    void Update()
    {
        switch (_state)
        {
            case CustomerState.WalkToOrder:
                OrderIfAtOrderPoint();
                break;
            case CustomerState.GoAway:
                DespawnIfAtSpawn();
                break;
            case CustomerState.Ordered:
                RotateTowardsLookPosition(Time.deltaTime);
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
    
    public void StartCustomerBehaviour(Vector3 orderPosition, Vector3 lookAt)
    {
        _state = CustomerState.WalkToOrder;
        _navMeshAgent.destination = orderPosition;
        _lookAtPosition = lookAt;
    }

    private void OrderIfAtOrderPoint()
    {
        if (Vector3.Distance(transform.position, _navMeshAgent.destination) <= distanceEpsilon) Order();
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
        _getAngryCoroutine = AngryInSeconds(10);
        StartCoroutine(_getAngryCoroutine);
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
            StopCoroutine(_getAngryCoroutine);
            orderText.text = "Thank you, exactly what I wanted";
            MoveToDespawn();
        }
        else
        {
            orderText.text = "That is not what I ordered!";
        }
    }

    private void MoveToDespawn()
    {
        _navMeshAgent.destination = _spawnPosition;
        _state = CustomerState.GoAway;
    }
}
