using UnityEngine;

public class RestState : EnemyStates
{
    float restTimer;

    public RestState(EnemyStateMachine enemy) : base(enemy) { }

    public override void Enter()
    {
        restTimer = 2f; // rest time
        enemy.rb.linearVelocity = Vector3.zero;
    }

    public override void Update()
    {
        restTimer -= Time.deltaTime;

        if (restTimer <= 0)
        {
            enemy.ChangeState(enemy.patrolState);
        }
    }
}