using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour
{
    [SerializeField] private float distanceEpsilon = 0.1f;

    [SerializeField] private TextMeshProUGUI orderText;
    private Vector3 _spawnPosition;
    
    private enum CustomerState
    {
        Waiting,
        WalkToOrder,
        Ordered,
        GoAway
    }
    // Start is called before the first frame update
    private NavMeshAgent _navMeshAgent;
    private CustomerState _state = CustomerState.Waiting;
    private RecipeData _requestedPotion;
    private IEnumerator _getAngryCoroutine;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _spawnPosition = transform.position;
    }

    void Start()
    {
        _requestedPotion = PotionKnowledgebase.Instance.RandomRecipe();
    }

    // Update is called once per frame
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
            default:
                break;
        }
    }

    public void StartCustomerBehaviour(Vector3 orderPosition)
    {
        _state = CustomerState.WalkToOrder;
        _navMeshAgent.destination = orderPosition;
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
        if (other.gameObject.CompareTag("Vial") && _state == CustomerState.Ordered)
        {
            Vial vial = other.gameObject.GetComponent<Vial>();
            Give(vial);
            Destroy(other.gameObject);
        }
    }

    private void Give(Vial vial)
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
