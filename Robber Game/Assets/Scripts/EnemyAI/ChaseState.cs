using UnityEngine;

public class ChaseState : EnemyStates
{
    public float timer;
    public ChaseState(EnemyStateMachine enemy) : base(enemy) { }

    public override void Update()
    {
        Chase();
        
    }

    private void Chase()
    {
        timer += Time.deltaTime;
        enemy.aiAgent.SetDestination(enemy.player.position);
        enemy.aiAgent.speed = enemy.chaseSpeed;
        if(timer >= enemy.chaseTimer && enemy.aiAgent.remainingDistance >= enemy.leaveDistance)
        {
            enemy.ChangeState(enemy.restState);
        }
        if(timer <= enemy.chaseTimer && enemy.aiAgent.remainingDistance <= enemy.leaveDistance)
        {
            enemy.ChangeState(enemy.attackState);
        }
    }
}