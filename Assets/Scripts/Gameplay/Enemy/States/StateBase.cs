using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase
{
    protected static readonly int State = Animator.StringToHash("State");
    public StateType stateType;
    
    protected FsmEnemyManager fsmManager;
    protected Animator animator;
    protected EnemySettingsSO enemySettingsSO;
    protected NavMeshAgent agent;

    public virtual void Initialize(FsmEnemyManager fsmManager, 
        Animator animator, 
        EnemySettingsSO enemySettingsSO, 
        NavMeshAgent agent)
    {
        this.fsmManager = fsmManager;
        this.animator = animator;
        this.enemySettingsSO = enemySettingsSO;
        this.agent = agent;
    }

    public virtual void OnEnter()
    {
        animator.SetInteger("state", (int)stateType);
    }
    public abstract void OnUpdate();
    public abstract void OnExit();
}
