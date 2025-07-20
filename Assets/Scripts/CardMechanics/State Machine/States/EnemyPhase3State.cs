using UnityEngine;

public class EnemyPhase3State : EnemyState
{
    public EnemyPhase3State(EnemyView enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("<color=magenta>ENTERING STATE: Phase 3</color>");
        Enemy.Phase3Instance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("<color=red>EXITING STATE: Phase 3</color>");
        Enemy.Phase3Instance.DoExitLogic();

    }
    public override void Reaction(EnemyTurnGA enemyTurnGA)
    {
        base.Reaction(enemyTurnGA);
        Debug.Log("I am Phase 3");
        Enemy.Phase3Instance.DoReactionLogic(enemyTurnGA);
    }
}
