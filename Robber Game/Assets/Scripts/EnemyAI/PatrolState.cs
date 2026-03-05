using UnityEngine;

public class PatrolState : EnemyStates
{
    int currentPoint;

    public PatrolState(EnemyStateMachine enemy) : base(enemy) { }

    public override void Enter()
    {
        currentPoint = 0;
    }

    public override void Update()
    {
        Transform target = enemy.patrolPoints[currentPoint];

        // Move toward patrol point
        Vector3 dir = (target.position - enemy.transform.position).normalized;
        enemy.rb.linearVelocity = new Vector3(dir.x * enemy.moveSpeed, enemy.rb.linearVelocity.y, dir.z * enemy.moveSpeed);

        // If close to patrol point → go to next
        if (Vector3.Distance(enemy.transform.position, target.position) < 1f)
        {
            currentPoint++;

            if (currentPoint >= enemy.patrolPoints.Length)
                currentPoint = 0;
        }

        // Detect player
        if (enemy.DistanceToPlayer() < enemy.chaseDistance)
        {
            enemy.ChangeState(enemy.chaseState);
        }
    }
}