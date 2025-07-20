using UnityEngine;

public class EnemyPhase1State : EnemyState
{
    public EnemyPhase1State(EnemyView enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("<color=cyan>ENTERING STATE: Phase 1</color>");
        Enemy.Phase1Instance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("<color=red>EXITING STATE: Phase 1</color>");
        Enemy.Phase1Instance.DoExitLogic();

    }
    public override void Reaction(EnemyTurnGA enemyTurnGA)
    {
        base.Reaction(enemyTurnGA);
        Debug.Log("I am in Phase 1");
        Enemy.Phase1Instance.DoReactionLogic(enemyTurnGA);
    }
}
