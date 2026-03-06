using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyStateMachine : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public NavMeshAgent aiAgent;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float chaseSpeed = 5f;

    [Header("Detection")]
    public float chaseDistance = 8f;
    public float chaseTimer;
    public float leaveDistance;

    [HideInInspector] public Rigidbody rb;

    EnemyStates currentState;

    public PatrolState patrolState;
    public ChaseState chaseState;
    public RestState restState;
    public AttackState attackState;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        patrolState = new PatrolState(this);
        chaseState = new ChaseState(this);
        restState = new RestState(this);
    }

    void Start()
    {
        ChangeState(patrolState);
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(EnemyStates newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    // Distance to player
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }
}