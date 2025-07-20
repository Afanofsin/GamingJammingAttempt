using UnityEngine;
public class CollectCardsQuest : Quest.QuestGoal
{
    public override string GetDescription()
    {
        return "Collect " + 5 + " cards";
    }
    public override void Initialize()
    {
        base.Initialize();
        ActionSystem.SubscribeReaction<CollectCardEvent>(OnPickUp, ReactionTiming.POST);
    }
    private void OnPickUp(CollectCardEvent collect)
    {
        CurrentAmount++;
        Evaluate();
    }
    public override void Complete()
    {   
        ActionSystem.UnsubscribeReaction<CollectCardEvent>(OnPickUp, ReactionTiming.POST);
        base.Complete();
    }
}

