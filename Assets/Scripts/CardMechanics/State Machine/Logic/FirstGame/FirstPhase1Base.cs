using UnityEngine;

[CreateAssetMenu(fileName = "FirstGame Phase 1", menuName = "Enemy/First/Phase 1")]
public class FirstPhase1Base : EnemyPhase1SOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoReactionLogic(EnemyTurnGA enemyTurnGA)
    {
        base.DoReactionLogic(enemyTurnGA);
    }

    public override void Initialize(EnemyView enemyView)
    {
        base.Initialize(enemyView);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
