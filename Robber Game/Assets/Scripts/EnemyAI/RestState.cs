using UnityEngine;

public class RestState : EnemyStates
{
    public RestState(EnemyStateMachine stateMachine)
        : base(stateMachine) { }

    public override void Enter()
    {
        // Start resting
    }

    public override void Update()
    {
        // Rest logic

        // Example:
        // if (restFinished)
        //     stateMachine.ChangeState(stateMachine.PatrolState);
    }

    public override void Exit()
    {
        // End rest
    }
}