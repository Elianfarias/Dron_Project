using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase
{
    protected static readonly int State = Animator.StringToHash("State");
    public StateType stateType;
    
    protected FsmNPCManager fsmManager;
    protected Animator animator;
    protected EnemySettingsSO enemySettingsSO;
    protected NavMeshAgent agent;
    protected GameObject player;
    protected bool isCivil;

    public virtual void Initialize(FsmNPCManager fsmManager, 
        Animator animator, 
        EnemySettingsSO enemySettingsSO, 
        NavMeshAgent agent,
        GameObject player,
        bool isCivil
        )
    {
        this.fsmManager = fsmManager;
        this.animator = animator;
        this.enemySettingsSO = enemySettingsSO;
        this.agent = agent;
        this.player = player;
        this.isCivil = isCivil;
    }

    public virtual void OnEnter()
    {
        animator.SetInteger(State, (int)stateType);
    }
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract void OnAnimatorIK(int layerIndex);

    protected bool IsPlayerNearby()
    {
        if (isCivil)
            return false;

        return Physics.CheckSphere(fsmManager.transform.position, enemySettingsSO.PlayerDetectionRadius, enemySettingsSO.PlayerLayer);
    }
}
