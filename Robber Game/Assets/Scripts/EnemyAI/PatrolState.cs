using UnityEngine;

public class PatrolState : EnemyStates
{
    int currentPoint;

    public PatrolState(EnemyStateMachine enemy) : base(enemy) { }

    public override void Enter()
    {
        currentPoint = Random.Range(0,enemy.patrolPoints.Length);
    }

    public override void Update()
    {
        Patrol();
        if (PlayerCheck())
        {
            enemy.ChangeState(enemy.chaseState);
        }
    }

    private void Patrol()
    {
        Transform target = enemy.patrolPoints[currentPoint];
        Vector3 targetPosition = target.position;
        enemy.aiAgent.SetDestination(targetPosition);

        if(enemy.aiAgent.remainingDistance < 0.5f)
        {
            //Change currentPoint
            currentPoint =  Random.Range(0 , enemy.patrolPoints.Length);
            target = enemy.patrolPoints[currentPoint];
            targetPosition = target.position;
            enemy.aiAgent.SetDestination(targetPosition);
        }
    }

    private bool PlayerCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, Vector3.forward, out hit, enemy.chaseDistance) && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}