using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DreamGame Phase 1", menuName = "Enemy/Dream/Phase 1")]
public class DreamGamePhase1 : EnemyPhase1SOBase
{
    float MoneyDamage = 10;
    public override void Initialize(EnemyView enemyView)
    {
        base.Initialize(enemyView);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        ActionSystem.SubscribeReaction<NoMoneyGA>(JobChange, ReactionTiming.POST);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        ActionSystem.UnsubscribeReaction<NoMoneyGA>(JobChange, ReactionTiming.POST);
    }

    public override void DoReactionLogic(EnemyTurnGA enemyTurnGA)
    {
        base.DoReactionLogic(enemyTurnGA);

        MoneyDamageGA moneyDamageGA = new(MoneyDamage, HeroSystem.Instance.HeroView, this.Enemy);
        ActionSystem.Instance.AddReaction(moneyDamageGA);
    }

    private void JobChange(NoMoneyGA noMoneyGA)
    {
        Debug.Log("I am changing to JOB");
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
