using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyStates currentState;

    // States
    public PatrolState PatrolState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public RestState RestState { get; private set; }

    private void Awake()
    {
        // Initialize states
        PatrolState = new PatrolState(this);
        ChaseState = new ChaseState(this);
        RestState = new RestState(this);
    }

    private void Start()
    {
        ChangeState(PatrolState); // Default state
    }

    private void Update()
    {
        currentState?.Update();
    }

    // Switch between states
    public void ChangeState(EnemyStates newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}