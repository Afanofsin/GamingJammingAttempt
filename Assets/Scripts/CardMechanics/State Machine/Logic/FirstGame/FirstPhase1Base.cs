using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstGame Phase 1", menuName = "Enemy/First/Phase 1")]
public class FirstPhase1Base : EnemyPhase1SOBase
{
    [field: SerializeField]
    public List<AutoTargetEffect> OtherEffects { get; private set; }
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
        foreach (var effect in OtherEffects)
        {
            List<CombatantView> targets = effect.TargetMode.GetTargets();
            PerformEffectsGA performEffectsGA = new(effect.Effect, targets, Enemy);
            ActionSystem.Instance.AddReaction(performEffectsGA);
        }
        float maxHealthPercentage = (float)Enemy.CurrentHealth / (float)Enemy.MaxHealth;
        if (maxHealthPercentage < 0.75f)
        {
            Enemy.AttackPower += 1;
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
