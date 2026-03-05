using UnityEngine;

// Base class for all enemy states
public abstract class EnemyStates
{
    protected EnemyStateMachine stateMachine;

    public EnemyStates(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    // Called when state starts
    public virtual void Enter() { }

    // Called every frame
    public virtual void Update() { }

    // Called when state exits
    public virtual void Exit() { }
}