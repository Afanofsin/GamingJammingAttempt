using UnityEngine;

public class EnemyPhase2State : EnemyState
{
    public EnemyPhase2State(EnemyView enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("<color=yellow>ENTERING STATE: Phase 2</color>");
        Enemy.Phase2Instance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("<color=red>EXITING STATE: Phase 2</color>");
        Enemy.Phase2Instance.DoExitLogic();

    }
    public override void Reaction(EnemyTurnGA enemyTurnGA)
    {
        base.Reaction(enemyTurnGA);
        Debug.Log("Phase 2");
        Enemy.Phase2Instance.DoReactionLogic(enemyTurnGA);
    }
}
