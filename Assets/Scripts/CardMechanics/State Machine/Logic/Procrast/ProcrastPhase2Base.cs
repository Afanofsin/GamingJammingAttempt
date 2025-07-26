using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Procrast Phase 2", menuName = "Enemy/Procrastination/Phase 2")]
public class ProcrastPhase2Base : EnemyPhase2SOBase
{
    [field: SerializeField]
    public List<AutoTargetEffect> OtherEffects { get; private set; }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        foreach (var effect in OtherEffects)
        {
            List<CombatantView> targets = effect.TargetMode.GetTargets();
            PerformEffectsGA performEffectsGA = new(effect.Effect, targets, Enemy);
            ActionSystem.Instance.AddReaction(performEffectsGA);
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoReactionLogic(EnemyTurnGA enemyTurnGA)
    {
        base.DoReactionLogic(enemyTurnGA);
        float maxHealthPercentage = (float)Enemy.CurrentHealth / (float)Enemy.MaxHealth;
        if (maxHealthPercentage < 0.5f)
        {
            Enemy.AttackPower += 4;
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
