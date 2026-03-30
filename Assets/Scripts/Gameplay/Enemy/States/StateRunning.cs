using UnityEngine;
using UnityEngine.AI;

public class StateRunning : StateBase
{
    private float _waitTimer;
    public override void Initialize(FsmEnemyManager fsmManager, Animator animator, EnemySettingsSO enemySettingsSO, NavMeshAgent agent)
    {
        base.Initialize(fsmManager, animator, enemySettingsSO, agent);

        this.stateType = StateType.Running;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        if (IsObstacleNearby() || agent.remainingDistance <= agent.stoppingDistance)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= enemySettingsSO.WaitTime)
            {
                MoveToRandomPosition();
                _waitTimer = 0f;
            }
        }
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    private bool IsObstacleNearby()
    {
        return Physics.CheckSphere(fsmManager.transform.position, enemySettingsSO.ObstacleDetectionRadius, enemySettingsSO.ObstacleLayer);
    }

    private void MoveToRandomPosition()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int randomIndex = Random.Range(0, navMeshData.vertices.Length);
        agent.SetDestination(navMeshData.vertices[randomIndex]);
    }
}
