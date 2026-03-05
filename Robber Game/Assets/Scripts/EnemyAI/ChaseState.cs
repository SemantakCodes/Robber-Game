using UnityEngine;

public class ChaseState : EnemyStates
{
    public ChaseState(EnemyStateMachine stateMachine)
        : base(stateMachine) { }

    public override void Enter()
    {
        // Start chasing
    }

    public override void Update()
    {
        // Chase logic

        // Example:
        // if (lostPlayer)
        //     stateMachine.ChangeState(stateMachine.RestState);
    }

    public override void Exit()
    {
        // Stop chasing
    }
}