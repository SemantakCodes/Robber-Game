using UnityEngine;

// Base state class
public abstract class EnemyStates
{
    protected EnemyStateMachine enemy;

    public EnemyStates(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}