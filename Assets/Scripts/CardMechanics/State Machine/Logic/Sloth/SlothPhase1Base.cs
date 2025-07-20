using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sloth Phase 1", menuName = "Enemy/Sloth/Phase1")]
public class SlothPhase1Base : EnemyPhase1SOBase
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
        Debug.Log("SO, Phase 1");
        float maxHealthPercentage = (float)Enemy.CurrentHealth / (float)Enemy.MaxHealth;
        if(maxHealthPercentage < 0.75f)
        {
            Enemy.UpdateAttackText();
            Enemy.StateMachine.ChangeState(Enemy.Phase2State);
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
