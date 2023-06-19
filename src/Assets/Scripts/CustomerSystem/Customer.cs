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
    [SerializeField] private float timeUntilAngry = 15f;
    [SerializeField] private Animator animator;

    [SerializeField] private float timeAfterReceived = 3f;
    
    
    private NavMeshAgent _navMeshAgent;
    private CustomerState _state = CustomerState.Waiting;
    private RecipeData _requestedPotion;
    private IEnumerator _getAngryCoroutine;
    
    

    private Vector3 _previousForward;
    private Transform _lookAt;
    private float _lookAtTimer = 0;

    private int orderPosition = -1;
    private bool hasLimit = true; // This lets us not have a time limit when in tutorial
    private Vector3 exitPosition;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Happy = Animator.StringToHash("Happy");

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        OrderPoint.CustomerArrived += HandleCustomerArrive;
    }


    private void OnDisable()
    {
        OrderPoint.CustomerArrived -= HandleCustomerArrive;
    }

    private void HandleCustomerArrive(int orderPoint)
    {
        print("A customer was said to arrive in: " + orderPoint);
        if (orderPoint == orderPosition)
        {
            print("The customer" + transform.name + " Arrived to its place #" + orderPoint);
            InteractionRaised?.Invoke(InteractionEvents.CustomerArrives);
            ChangeState();
        }
    }

    public void StartTutorialCustomer(Vector3 orderWorldPosition, Transform lookAt, int whichOrderPosition, Vector3 exitWorldPosition)
    {
        _state = CustomerState.WalkingToOrder;
        _navMeshAgent.destination = orderWorldPosition;
        _lookAt = lookAt;
        orderPosition = whichOrderPosition;
        hasLimit = false;
        exitPosition = exitWorldPosition;
        
        if (animator is not null)
        {
            Debug.Log("Set bool Walking to true");
            animator.SetBool(Walking, true);
        }
    }


    void Start()
    {
        _requestedPotion = PotionKnowledgebase.Instance.RandomRecipe();
    }

    private bool isRotating = false;

    public void ChangeState() // TODO: Check how to make this better. RN it is public so the Exit point simply access the customer and triggers a change state, but that starts smelling
    {
        switch (_state)
        {
            case CustomerState.WalkingToOrder: // If it is walking to order and change state is called, then it should now order
                Order();
                isRotating = true;
                
                break;
            case CustomerState.Ordered:
                break;
            case CustomerState.GoAway:
                print("Should not arrive here");
                // Despawn();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(isRotating) RotateTowardsLookPosition(Time.deltaTime);
    }
    private void RotateTowardsLookPosition(float delta)
    {
        if(_lookAtTimer >= 1) return; // This makes it stop turning after 1 second
        _lookAtTimer += delta;
        Vector3 directionVector =  new Vector3(_lookAt.position.x - transform.position.x, 0, _lookAt.position.z - transform.position.z);
        transform.forward = Vector3.Lerp(_previousForward, directionVector, _lookAtTimer);
    }
    
    public void StartCustomerBehaviour(Vector3 orderWorldPosition, Transform lookAt, int whichOrderPosition, Vector3 exitWorldPosition)
    {
        _state = CustomerState.WalkingToOrder;
        _navMeshAgent.destination = orderWorldPosition;
        _lookAt = lookAt;
        orderPosition = whichOrderPosition;
        hasLimit = true;
        exitPosition = exitWorldPosition;

        if (animator is not null)
        {
            animator.SetBool(Walking, true);
        }
        
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
            _getAngryCoroutine = AngryInSeconds(timeUntilAngry);
            StartCoroutine(_getAngryCoroutine);
        }

        if (animator is not null)
        {
            animator.SetBool(Walking, false);
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
            StartCoroutine(MoveToDespawnIn(timeAfterReceived));
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

    private IEnumerator MoveToDespawnIn(float seconds)
    {
        if (animator is not null)
        {
            animator.SetTrigger(Happy);
        }

        yield return new WaitForSeconds(seconds);
        MoveToDespawn();
    }
    
    private void MoveToDespawn()
    {
        _navMeshAgent.destination = exitPosition;
        if (animator is not null)
        {
            animator.SetBool(Walking, true);
        }
        // _state = CustomerState.GoAway;
    }
}
