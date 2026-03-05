using UnityEngine;

public class PatrolState : EnemyStates
{
    public PatrolState(EnemyStateMachine stateMachine)
        : base(stateMachine) { }

    public override void Enter()
    {
        // Start patrolling
    }

    public override void Update()
    {
        // Patrol logic here

        // Example transition:
        // if (playerDetected)
        //     stateMachine.ChangeState(stateMachine.ChaseState);
    }

    public override void Exit()
    {
        // Cleanup patrol
    }
}