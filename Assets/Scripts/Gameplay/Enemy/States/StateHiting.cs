using UnityEngine;
using UnityEngine.AI;

public class StateHiting : StateBase
{
    public override void Initialize(FsmEnemyManager fsmManager, Animator animator, EnemySettingsSO enemySettingsSO, NavMeshAgent agent)
    {
        base.Initialize(fsmManager, animator, enemySettingsSO, agent);

        this.stateType = StateType.Hiting;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }
}