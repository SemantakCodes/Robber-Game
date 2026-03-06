using UnityEngine;

public class ChaseState : EnemyStates
{
    public ChaseState(EnemyStateMachine enemy) : base(enemy) { }

    public override void Update()
    {
        Vector3 dir = (enemy.player.position - enemy.transform.position).normalized;

        enemy.rb.linearVelocity = new Vector3(dir.x * enemy.chaseSpeed, enemy.rb.linearVelocity.y, dir.z * enemy.chaseSpeed);

        // If player escapes
        if (enemy.DistanceToPlayer() > enemy.chaseDistance + 3f)
        {
            enemy.ChangeState(enemy.restState);
        }
        
    }

    private void Chase()
    {
        
    }
}