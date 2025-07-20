using UnityEngine;

[CreateAssetMenu(fileName = "Sloth Phase 2", menuName = "Enemy/Sloth/Phase2")]
public class SlothPhase2Base : EnemyPhase2SOBase
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
        Debug.Log("SO, Phase 2");
        float maxHealthPercentage = (float)Enemy.CurrentHealth / (float)Enemy.MaxHealth;
        if (maxHealthPercentage < 0.5f)
        {
            Enemy.AttackPower += 2;
            Enemy.UpdateAttackText();
            Enemy.StateMachine.ChangeState(Enemy.Phase3State);
        }
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
