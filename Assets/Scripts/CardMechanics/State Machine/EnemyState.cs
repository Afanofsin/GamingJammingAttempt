using UnityEngine;

public class EnemyState 
{
    protected EnemyView Enemy;
    protected EnemyStateMachine StateMachine;

    public EnemyState(EnemyView enemy, EnemyStateMachine stateMachine   )
    {
        Enemy = enemy;
        StateMachine = stateMachine;
    }

    public virtual void EnterState()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(Reaction, ReactionTiming.POST);
    }
    public virtual void ExitState() 
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(Reaction, ReactionTiming.POST);
    }
    public virtual void Reaction(EnemyTurnGA enemyTurnGA)
    {
    }
}
